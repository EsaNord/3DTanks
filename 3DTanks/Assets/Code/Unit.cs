using Tanks3D.persistance;
using UnityEngine;

namespace Tanks3D
{
    public abstract class Unit : MonoBehaviour, IDamageReciever
    {
        #region Statics
        private static int s_idCounter = 0;

        public static int GetNextId()
        {
            var allUnits = FindObjectsOfType<Unit>();
            foreach (var unit in allUnits)
            {
                if (unit.Id >= s_idCounter)
                    s_idCounter = unit.Id + 1;
            }

            return s_idCounter++;
        }
        #endregion

        [SerializeField]
        public float _moveSpeed;
        [SerializeField]
        private float _turnSpeed;
        [SerializeField]
        private int m_iStartingHealth;
        [SerializeField, HideInInspector]
        private int _id = -1;

        private IMover _mover;

        public Weapon Weapon
        {
            get;
            protected set;
        }          

        public IMover Mover { get { return _mover; } }

        public Health Health { get; protected set; }
        public int Id { get { return _id; } private set { _id = value; } }

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

        public void RequestId()
        {
            if (Id < 0)
            {
                Id = GetNextId();
            }
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

        public virtual UnitData GetUnitData()
        {
            return new UnitData
            {
                Health = Health.CurrentHealth,
                Position = transform.position,
                YRotation = transform.rotation.y,
                Id = Id
            };
        }

        public virtual void SetUnitData(UnitData data)
        {
            Health.SetHealth(data.Health);
            transform.position = data.Position;
            transform.eulerAngles = new Vector3(0, data.YRotation, 0);
        }
    }
}