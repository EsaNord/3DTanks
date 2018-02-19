using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class Projectile : MonoBehaviour
    {        
        [SerializeField, Range(0, 100)]
        private int m_iDamage;
        [SerializeField]
        private float m_fShootingForce;
        [SerializeField]
        private float m_fExplosionForce;
        [SerializeField]
        private float m_fExplosionRadius;

        [SerializeField, HideInInspector]
        private int m_iHitmask;

        [SerializeField]
        private ProjectileType type;

        private Weapon _weapon;
        private Rigidbody _rigidBody;
        private System.Action<Projectile> _collisionCallBack;

        [Flags]
        public enum ProjectileType
        {
            none = 0,
            player = 1,
            enemy = 1 << 1,
            neutral = 1 << 2
        }

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

        public void Init(System.Action<Projectile> collisionCallBack)
        {
            _collisionCallBack = collisionCallBack;
        }

        public void Launch(Vector3 direction, float objectSpeed)
        {
            // TODO Add particle effects     
            
            RigidBody.AddForce(direction.normalized * (m_fShootingForce + objectSpeed), ForceMode.Impulse);            
        }

        protected void OnCollisionEnter(Collision collision)
        {
            // TODO Add particle effects
            ApplyDamage();
            RigidBody.velocity = Vector3.zero;
            _collisionCallBack(this);
            Debug.Log("HIT: " + collision);
        }

        private void ApplyDamage()
        {
            List<IDamageReciever> alreadyDamaged = new List<IDamageReciever>(); 
            Collider[] damageRecievers = Physics.OverlapSphere(transform.position, m_fExplosionRadius, m_iHitmask);
            for (int i = 0; i < damageRecievers.Length; i++)
            {
                IDamageReciever damageReciever = damageRecievers[i].GetComponentInParent<IDamageReciever>();
                if (damageReciever != null && !alreadyDamaged.Contains(damageReciever))
                {
                    alreadyDamaged.Add(damageReciever);
                    damageReciever.TakeDamage(m_iDamage);                    
                    // TODO Add explosion force
                }
            }
        }
    }
}