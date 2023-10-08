using TMPro;
using UnityEngine;

namespace Points.View
{
    public class PointsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            _text.text = "0";
        }

        public void Display(int amount)
        {
            _text.text = amount.ToString();
        }
    }
}