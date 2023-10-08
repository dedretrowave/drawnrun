using System;
using System.Collections.Generic;
using CharacterGroup.Character.View;
using Interactables;

namespace CharacterGroup.Presenter
{
    public class CharacterGroupPresenter
    {
        private Model.CharacterGroup _characterGroup;

        public event Action NoCharactersLeft;
        public event Action PointPickup;
        public event Action FinishLineReached;

        public CharacterGroupPresenter(List<CharacterView> initialCharacters)
        {
            _characterGroup = new(initialCharacters);

            _characterGroup.SubscribeToCollideAll(OnCharacterCollide);
            _characterGroup.NoCharactersLeft += OnNoCharactersLeft;
        }

        public void Disable()
        {
            _characterGroup.UnsubscribeFromCollideAll(OnCharacterCollide);
            _characterGroup.NoCharactersLeft -= OnNoCharactersLeft;
        }

        private void OnCharacterCollide(CharacterView character, Interactable interactable)
        {
            switch (interactable.Type)
            {
                case InteractableType.Obstacle:
                    _characterGroup.UnsubscribeFromCollide(character, OnCharacterCollide);
                    _characterGroup.Remove(character);
                    character.Destroy();
                    break;
                case InteractableType.Point:
                    PointPickup?.Invoke();
                    break;
                case InteractableType.Character:
                    _characterGroup.Add(character);
                    _characterGroup.SubscribeToCollide(character, OnCharacterCollide);
                    break;
                case InteractableType.FinishLine:
                    FinishLineReached?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnNoCharactersLeft()
        {
            NoCharactersLeft?.Invoke();
        }
    }
}