using UnityEngine;

namespace Helpers
{
    public class ParticlesInstantiator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;

        public void Instantiate()
        {
            Instantiate(_particles, transform.position, Quaternion.identity);
        }
    }
}