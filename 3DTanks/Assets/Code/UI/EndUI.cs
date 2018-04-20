using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.UI
{
    public class EndUI : MonoBehaviour
    {
        [SerializeField]
        private VictoryUIItem _victoryUIItem;
        [SerializeField]
        private DefeatUIItem _defatUIItem;        

        public void Init()
        {
            Debug.Log("End UI Initialized");
        }

        /// <summary>
        /// Initializes endgame ui items
        /// </summary>
        public void InitItems()
        {
            _victoryUIItem.Init();
            _victoryUIItem.gameObject.SetActive(false);
            _defatUIItem.Init();
            _defatUIItem.gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets victory ui item active.
        /// </summary>
        public void Victory()
        {
            _victoryUIItem.gameObject.SetActive(true);
        }

        /// <summary>
        /// Sets defeat ui item active.
        /// </summary>
        public void Defeat()
        {
            _defatUIItem.gameObject.SetActive(true);
        }
    }
}