﻿using System.Collections;
using System.Collections.Generic;
using Tanks3D.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks3D.UI
{
    public class HealthUIItem : MonoBehaviour
    {
        private Unit _unit;
        private Text _text;
        private ISubscription<UnitDiedMessage> _unitDiedsubscription;

        public bool IsEnemy { get { return _unit != null && _unit is EnemyUnit; } }

        public void Init(Unit unit)
        {
            _unit = unit;
            _text = GetComponentInChildren<Text>();
            _text.color = IsEnemy ? Color.red : Color.green;
            _unit.Health.HealthChanged += OnUnitHealtChanged;
            //_unit.Health.UnitDied += OnUnitDied;
            _unitDiedsubscription = GameManager.Instance.MessageBus.Subscribe<UnitDiedMessage>(OnUnitDied);
            SetText(_unit.Health.CurrentHealth);
        }

        private void OnDestroy()
        {
            UnregisterEventListeners();
        }

        private void OnUnitDied(UnitDiedMessage obj)
        {
            if (obj.DeadUnit == _unit)
               UnregisterEventListeners();
        }

        private void UnregisterEventListeners()
        {
            _unit.Health.HealthChanged -= OnUnitHealtChanged;
            GameManager.Instance.MessageBus.UnSubscribe(_unitDiedsubscription);
            //_unit.Health.UnitDied -= OnUnitDied;
        }

        private void OnUnitHealtChanged(Unit unit, int health)
        {
            SetText(health);
        }

        private void SetText(int health)
        {
            _text.text = string.Format("{0} health: {1}", _unit.name, health);
        }
    }
}