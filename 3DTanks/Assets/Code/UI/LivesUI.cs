using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.UI
{
    public class LivesUI : MonoBehaviour
    {
        [SerializeField]
        private LivesUIItem _livesUIItem;

        /// <summary>
        /// Initalization of Lives ui.
        /// </summary>
        public void Init()
        {
            Debug.Log("Lives UI Initialized");
        }

        /// <summary>
        /// Initializes lives ui item and activates it.
        /// </summary>
        /// <param name="unit"> Unit which lives are displayed (player)</param>
        public void SetLivesItem(Unit unit)
        {
            _livesUIItem.Init(unit);
            _livesUIItem.gameObject.SetActive(true);
        }
    }
}