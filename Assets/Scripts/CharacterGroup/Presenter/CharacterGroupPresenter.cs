using System;
using System.Collections.Generic;
using CharacterGroup.Character.View;
using DG.Tweening;
using Interactables;
using UnityEngine;
using UnityEngine.Splines;

namespace CharacterGroup.Presenter
{
    public class CharacterGroupPresenter
    {
        private List<CharacterView> _characters;

        public event Action NoCharactersLeft;
        public event Action PointPickup;
        public event Action FinishLineReached;

        public CharacterGroupPresenter(List<CharacterView> initialCharacters)
        {
            _characters = new(initialCharacters);

            _characters.ForEach(character => character.Collide += OnCharacterCollide);
        }

        public void Disable()
        {
            _characters.ForEach(character => character.Collide -= OnCharacterCollide);
        }

        private void OnCharacterCollide(CharacterView character, Interactable interactable)
        {
            switch (interactable.Type)
            {
                case InteractableType.Obstacle:
                    if (_characters.Contains(character))
                    {
                        _characters.Find(thisCharacter => thisCharacter.Equals(character)).Collide -= OnCharacterCollide;
                    }

                    Remove(character);
                    character.Destroy();
                    break;
                case InteractableType.Point:
                    PointPickup?.Invoke();
                    break;
                case InteractableType.Character:
                    if (_characters.Contains(character))
                    {
                        _characters.Find(thisCharacter => thisCharacter.Equals(character)).Collide += OnCharacterCollide;
                    }
                    
                    Add(character);
                    break;
                case InteractableType.FinishLine:
                    FinishLineReached?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void DanceAll()
        {
            _characters.ForEach(character => character.PlayDance());
        }
        
        public void MoveToPointsAll(BezierKnot[] places)
        {
            int j = 0;
            
            for (var i = 0; i < _characters.Count; i++)
            {
                CharacterView character = _characters[i];

                if (j >= places.Length)
                {
                    j = 0;
                }

                // character.transform.localPosition = places[j].Position;
                character.transform.DOLocalMove(places[j].Position, GlobalSettings.CHARACTER_TWEEN_MOVE_SPEED);
                character.PlayRun();
                j++;
            }
        }

        private void Add(CharacterView character)
        {
            _characters.Add(character);
        }

        private void Remove(CharacterView character)
        {
            _characters.Remove(character);

            if (_characters.Count == 0)
            {
                NoCharactersLeft?.Invoke();
            }
        }
    }
}