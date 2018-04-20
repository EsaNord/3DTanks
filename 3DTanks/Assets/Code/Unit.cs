using Tanks3D.persistance;
using UnityEngine;
using Tanks3D.Messaging;

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
        private int _startingHealth;
        [SerializeField, Tooltip("Units spawn point")]
        private Transform _spawnPoint;
        [SerializeField, Tooltip("Player lives")]
        private int _lives;
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
        public int StartingHealth { get { return _startingHealth; } }
        public Vector3 SpawnPoint { get { return _spawnPoint.position; } }

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

            Health = new Health(this, _startingHealth, _lives);
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

        /// <summary>
        /// Method is only called when player dies.
        /// </summary>
        /// <param name="unit">Player unit</param>
        protected virtual void HandleUnitDied(Unit unit)
        {
            GameManager.Instance.MessageBus.Publish(new UnitDiedMessage(this));
            GameManager.Instance.PlayerLost();
            gameObject.SetActive(false);            
        }

        public virtual UnitData GetUnitData()
        {
            return new UnitData
            {
                Health = Health.CurrentHealth,
                Position = transform.position,
                YRotation = transform.rotation.y,
                Id = Id,
                PlayerScore = GameManager.Instance.score.CurrentScore,
                Lives = Health.CurrentLives
            };
        }

        public virtual void SetUnitData(UnitData data)
        {
            Health.SetHealth(data.Health);
            transform.position = data.Position;
            transform.eulerAngles = new Vector3(0, data.YRotation, 0);
            GameManager.Instance.score.CurrentScore = data.PlayerScore;
            Health.SetLives(data.Lives);
        }

        /// <summary>
        /// Resets unit position, health, lives and sets it active.
        /// Also publishes unit reset message so health ui items is also reset.
        /// </summary>
        public void ResetUnit()
        {
            Health.SetHealth(_startingHealth);
            Health.SetLives(_lives);
            transform.position = SpawnPoint;
            gameObject.SetActive(true);
            GameManager.Instance.MessageBus.Publish(new UnitReset(this));
        }
    }
}