using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private ScoreUIItem _scoreUIItem;

        /// <summary>
        /// Initialization of score ui.
        /// </summary>
        public void Init()
        {
            Debug.Log("Score UI Initialized");           
        }      
        
        /// <summary>
        /// Initializas score ui item and activates it.
        /// </summary>
        public void SetScoreItem()
        {
            _scoreUIItem.Init();
            _scoreUIItem.gameObject.SetActive(true);
        }
    }
}