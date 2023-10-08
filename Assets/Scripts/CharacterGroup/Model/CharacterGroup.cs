using System;
using System.Collections.Generic;
using System.Linq;
using CharacterGroup.Character.View;
using Interactables;
using UnityEngine;

namespace CharacterGroup.Model
{
    public class CharacterGroup
    {
        private List<CharacterView> _characters;

        public event Action NoCharactersLeft;

        public CharacterGroup(List<CharacterView> characters)
        {
            _characters = new(characters);
        }

        public void SubscribeToCollide(CharacterView character, Action<CharacterView, Interactable> callback)
        {
            if (_characters.Contains(character))
            {
                _characters.Find(thisCharacter => thisCharacter.Equals(character)).Collide += callback;
            }
        }
        
        public void UnsubscribeFromCollide(CharacterView character, Action<CharacterView, Interactable> callback)
        {
            if (_characters.Contains(character))
            {
                _characters.Find(thisCharacter => thisCharacter.Equals(character)).Collide -= callback;
            }
        }

        public void SubscribeToCollideAll(Action<CharacterView, Interactable> callback)
        {
            _characters.ForEach(character => character.Collide += callback);
        }

        public void UnsubscribeFromCollideAll(Action<CharacterView, Interactable> callback)
        {
            _characters.ForEach(character => character.Collide -= callback);
        }

        public void Add(CharacterView character)
        {
            _characters.Add(character);
        }

        public void Remove(CharacterView character)
        {
            _characters.Remove(character);

            if (_characters.Count == 0)
            {
                NoCharactersLeft?.Invoke();
            }
        }
    }
}