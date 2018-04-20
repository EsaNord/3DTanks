using System.Collections;
using System.Collections.Generic;
using Tanks3D.Localization;
using Tanks3D.Messaging;
using UnityEngine;
using UnityEngine.UI;
using L10n = Tanks3D.Localization.Localization;

namespace Tanks3D.UI
{
    public class HealthUIItem : MonoBehaviour
    {
        private Unit _unit;
        private Text _text;
        private ISubscription<UnitDiedMessage> _unitDiedsubscription;
        private ISubscription<UnitReset> _unitResetSub;
        private const string HealthKey = "health";        

        public bool IsEnemy { get { return _unit != null && _unit is EnemyUnit; } }

        public void Init(Unit unit)
        {
            L10n.LanguageLoaded += OnLanguageChange;
            _unit = unit;
            _text = GetComponentInChildren<Text>();

            _text.color = IsEnemy ? Color.red : Color.green;
            _unit.Health.HealthChanged += OnUnitHealtChanged;
            
            //_unit.Health.UnitDied += OnUnitDied;
            _unitDiedsubscription = GameManager.Instance.MessageBus.Subscribe<UnitDiedMessage>(OnUnitDied);
            _unitResetSub = GameManager.Instance.MessageBus.Subscribe<UnitReset>(OnUnitReset);

            SetText(_unit.Health.CurrentHealth);
        }

        private void OnDestroy()
        {
            UnregisterEventListeners();
        }

        private void OnUnitDied(UnitDiedMessage obj)
        {
            if (obj.DeadUnit == _unit)
            {
                UnregisterEventListeners();
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// If player continues after defeat healt UI item is set to show players hp again.
        /// </summary>
        /// <param name="unit"></param>
        private void OnUnitReset(UnitReset unit)
        {
            if (unit.ResetedUnit == _unit)
            {
                L10n.LanguageLoaded += OnLanguageChange;
                _unit.Health.HealthChanged += OnUnitHealtChanged;
                gameObject.SetActive(true);
                SetText(_unit.Health.CurrentHealth);
                _unitDiedsubscription = GameManager.Instance.MessageBus.Subscribe<UnitDiedMessage>(OnUnitDied);
            }
        }

        private void UnregisterEventListeners()
        {
            _unit.Health.HealthChanged -= OnUnitHealtChanged;
            L10n.LanguageLoaded -= OnLanguageChange;

            if (!GameManager.IsClosing)
                GameManager.Instance.MessageBus.UnSubscribe(_unitDiedsubscription);
            //_unit.Health.UnitDied -= OnUnitDied;
        }

        private void OnUnitHealtChanged(Unit unit, int health)
        {
            SetText(health);
        }

        private void OnLanguageChange( LangCode currentLang)
        {
            SetText(_unit.Health.CurrentHealth);
        }

        private void SetText(int health)
        {
            string UnitKey = IsEnemy? "enemy" : "player";

            string translation = L10n.CurrentLanguage.GetTranslation(HealthKey);
            string unitTranslation = L10n.CurrentLanguage.GetTranslation(UnitKey);

            _text.text = string.Format(translation, unitTranslation, health);
        }
    }
}