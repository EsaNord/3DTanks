using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Tanks3D
{
    public class Health
    {
        private int _currentHealth;
        private int _currentLives;

        public event Action<Unit> UnitDied;
        public event Action<Unit, int> HealthChanged;
        public event Action<int> LivesChanged;
        
        public int CurrentHealth
        {
            get { return _currentHealth; }
            protected set
            {    _currentHealth = value;
                if (HealthChanged != null)
                {
                    HealthChanged(Owner, _currentHealth);
                }
                OnPropertyChanged(() => CurrentHealth);
            }
        }

        /// <summary>
        /// Current player lives.
        /// </summary>
        public int CurrentLives
        {
            get { return _currentLives; }
            protected set
            {
                _currentLives = value;
                if (LivesChanged != null)
                {
                    LivesChanged(_currentLives);
                }
            }
        }

        public Unit Owner { get; private set; }

        public Health(Unit owner, int startingHealth)
        {
            Owner = owner;
            CurrentHealth = startingHealth;            
        }

        /// <summary>
        /// Second constructor for Health
        /// </summary>
        /// <param name="owner">Scripts owner unit</param>
        /// <param name="startingHealth">Units starting health</param>
        /// <param name="lives">Units starting lives</param>
        public Health(Unit owner, int startingHealth, int lives)
        {
            Owner = owner;
            CurrentHealth = startingHealth;
            CurrentLives = lives;            
        }

        /// <summary>
        /// Applies damage to the Unit.
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        /// <returns>True, if the unit dies. False otherwise</returns>
        public virtual bool TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, CurrentHealth);
            bool didDie = CurrentHealth == 0;
            if (didDie)
            {                
                if (Owner.GetComponent<PlayerUnit>() != null)
                {
                    // If owner is player and has lives left
                    // current lives amount is decreased, healt is resetted
                    // and player position is set to players spawn position
                    if (CurrentLives > 0)
                    {
                        CurrentLives--;
                        Owner.transform.position = Owner.SpawnPoint;
                        CurrentHealth = Owner.StartingHealth;
                    }
                    else
                    {
                        RaiseUnitDiedEvent();
                    }
                }
                else
                {
                    // If owner is enemy unit, it's health is resetted and position
                    // is set to enemy spawn position, also state is set to patrol.
                    Owner.transform.position = Owner.SpawnPoint;
                    CurrentHealth = Owner.StartingHealth;
                    Owner.GetComponent<EnemyUnit>().PerformTransition(AI.AIStateType.Patrol);                    
                }
            }
            return didDie;
        }

        protected void RaiseUnitDiedEvent()
        {
            if (UnitDied != null)
            {
                UnitDied(Owner);
            }
        }

        public void SetHealth(int health)
        {
            CurrentHealth = health;
        }

        /// <summary>
        /// Sets lives
        /// </summary>
        /// <param name="lives">amount of lives</param>
        public void SetLives(int lives)
        {
            CurrentLives = lives;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyLambda)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Utils.Utils.GetPropertyName(propertyLambda)));
        }
    }
}