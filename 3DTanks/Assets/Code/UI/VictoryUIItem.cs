using System.Collections;
using System.Collections.Generic;
using Tanks3D.Localization;
using UnityEngine;
using UnityEngine.UI;
using L10n = Tanks3D.Localization.Localization;

namespace Tanks3D.UI
{
    public class VictoryUIItem : MonoBehaviour
    {
        [SerializeField]
        private Text _yesText;
        [SerializeField]
        private Text _noText;
        [SerializeField]
        private Text _infoText;

        private const string YesKey = "yes";
        private const string NoKey = "no";
        private const string InfoWonKey = "won";

        /// <summary>
        /// Victory Ui items initialization.
        /// </summary>
        public void Init()
        {
            L10n.LanguageLoaded += OnLanguageChange;
            SetText();
        }  
        
        public void Activate()
        {
            Debug.Log("Victory");
        }

        /// <summary>
        /// Updates score text on screen when language is changed.
        /// </summary>
        /// <param name="currentLang">Current language code</param>
        private void OnLanguageChange(LangCode currentLang)
        {
            SetText();
        }

        /// <summary>
        /// Sets text in victory ui's items.
        /// </summary>
        private void SetText()
        {
            string infoTranslation = L10n.CurrentLanguage.GetTranslation(InfoWonKey);
            string yesTranslation = L10n.CurrentLanguage.GetTranslation(YesKey);
            string noTranslation = L10n.CurrentLanguage.GetTranslation(NoKey);

            _infoText.text = string.Format(infoTranslation);
            _yesText.text = string.Format(yesTranslation);
            _noText.text = string.Format(noTranslation);
        }

        private void OnDestroy()
        {
            UnregisterEventListeners();
        }

        /// <summary>
        /// Stops listening event listeners.
        /// </summary>
        private void UnregisterEventListeners()
        {            
            L10n.LanguageLoaded -= OnLanguageChange;
        }
    }
}