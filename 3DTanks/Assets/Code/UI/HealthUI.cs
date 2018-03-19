using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField]
        private HealthUIItem _healthUIItemPrefab;

        public void Init()
        {
            Debug.Log("Health UI Initialized");
        }

        public void AddUnit(Unit unit)
        {
            var healthItem = Instantiate(_healthUIItemPrefab, transform);
            healthItem.Init(unit);
            healthItem.gameObject.SetActive(true);
        }
    }
}