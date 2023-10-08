using UnityEngine;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private InteractableType _type;

        public InteractableType Type => _type;
    }
}