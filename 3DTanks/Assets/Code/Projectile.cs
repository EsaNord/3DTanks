using UnityEngine;

namespace Tanks3D
{
    public class Projectile : MonoBehaviour
    {        
        [SerializeField]
        private float _damage;
        [SerializeField]
        private float _shootingForce;
        [SerializeField]
        private float _explosionForce;
        [SerializeField]
        private float _explosionRadius;

        private Weapon _weapon;
        private Rigidbody _rigidBody;

        public Rigidbody RigidBody
        {
            get
            {
                if (_rigidBody == null)
                {
                    _rigidBody = gameObject.GetOrAddComponent<Rigidbody>();
                }
                return _rigidBody;
            }
        }

        public void Init(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void Launch(Vector3 direction)
        {
            // TODO Add particle effects
            RigidBody.AddForce(direction.normalized * _shootingForce, ForceMode.Impulse);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            // TODO Add particle effects
            // TODO apply damage to target            
            RigidBody.velocity = Vector3.zero;
            _weapon.ProjectileHit(this);
            Debug.Log("HIT: " + collision);
        }
    }
}