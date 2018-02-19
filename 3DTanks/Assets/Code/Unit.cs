using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public abstract class Unit : MonoBehaviour, IDamageReciever
    {
        [SerializeField]
        public float _moveSpeed;
        [SerializeField]
        private float _turnSpeed;
        [SerializeField]
        private int m_iStartingHealth;

        private IMover _mover;

        public Weapon Weapon
        {
            get;
            protected set;
        }          

        public IMover Mover { get { return _mover; } }

        public Health Health { get; protected set; }

        protected void Awake()
        {
            Init();
        }

        protected void OnDestroy()
        {
            Health.UnitDied -= HandleUnitDied;
        }

        public virtual void Init()
        {
            _mover = gameObject.GetOrAddComponent<TransformMover>();
            _mover.Init(_moveSpeed, _turnSpeed);

            Weapon = GetComponentInChildren<Weapon>();
            if (Weapon != null)
            {
                Weapon.Init(this);
            }

            Health = new Health(this, m_iStartingHealth);
            Health.UnitDied += HandleUnitDied;
        }

        public virtual void Clear()
        {

        }

        // An abstract method has to be defined in a non-abstract child class.
        protected abstract void Update();

        public void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);
        }

        protected virtual void HandleUnitDied(Unit unit)
        {
            gameObject.SetActive(false);            
        }
    }
}