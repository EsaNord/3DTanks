﻿using System.Collections;
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

        public event Action<Unit> UnitDied;
        public event Action<Unit, int> HealthChanged;
        
        public int CurrentHealth {
            get { return _currentHealth; }
            protected set { _currentHealth = value;
                if (HealthChanged != null)
                {
                    HealthChanged(Owner, _currentHealth);
                }
                OnPropertyChanged(() => CurrentHealth);
            }
        }

        public Unit Owner { get; private set; }

        public Health(Unit owner, int startingHealth)
        {
            Owner = owner;
            CurrentHealth = startingHealth;           
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
                RaiseUnitDiedEvent();
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyLambda)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Utils.Utils.GetPropertyName(propertyLambda)));
        }
    }
}