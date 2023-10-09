using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private InteractableType _type;
        [SerializeField] private UnityEvent _interact;

        public InteractableType Type => _type;

        public void Interact()
        {
            _interact.Invoke();
        }
    }
}