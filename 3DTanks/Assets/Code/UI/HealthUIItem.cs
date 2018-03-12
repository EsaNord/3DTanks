using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks3D.UI
{
    public class HealthUIItem : MonoBehaviour
    {
        private Unit _unit;
        private Text _text;

        public bool IsEnemy { get { return _unit != null && _unit is EnemyUnit; } }

        public void Init(Unit unit)
        {
            _unit = unit;
            _text = GetComponentInChildren<Text>();
            _text.color = IsEnemy ? Color.red : Color.green;
            _unit.Health.HealthChanged += OnUnitHealtChanged;
            _unit.Health.UnitDied += OnUnitDied;
            SetText(_unit.Health.CurrentHealth);
        }

        private void OnDestroy()
        {
            UnregisterEventListeners();
        }

        private void OnUnitDied(Unit obj)
        {
            UnregisterEventListeners();
        }

        private void UnregisterEventListeners()
        {
            _unit.Health.HealthChanged -= OnUnitHealtChanged;
            _unit.Health.UnitDied -= OnUnitDied;
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