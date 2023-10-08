using System;
using Interactables;
using UnityEngine;

namespace CharacterGroup.Character.View
{
    public class CharacterView : MonoBehaviour
    {
        public event Action<CharacterView, Interactable> Collide;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Interactable interactable)) return;
            
            Collide?.Invoke(this, interactable);
        }
    }
}