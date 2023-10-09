using System;
using Interactables;
using UnityEngine;

namespace CharacterGroup.Character.View
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public event Action<CharacterView, Interactable> Collide;

        public void PlayRun()
        {
            _animator.SetBool(CharacterAnimations.RunKey, true);
        }

        public void PlayDance()
        {
            _animator.SetBool(CharacterAnimations.DanceKey, true);
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Interactable interactable)) return;

            interactable.Interact();
            Collide?.Invoke(this, interactable);
        }
    }
}