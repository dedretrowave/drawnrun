using UnityEngine;

namespace Player 
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Vector3 _direction;

        public void Move()
        {
            _direction = Vector3.forward;
        }

        public void Stop()
        {
            _direction = Vector3.zero;
        }

        private void FixedUpdate()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
        }
    }
}