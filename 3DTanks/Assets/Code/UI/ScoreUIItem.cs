using Tanks3D.Localization;
using UnityEngine;
using UnityEngine.UI;
using L10n = Tanks3D.Localization.Localization;

namespace Tanks3D.UI
{
    public class ScoreUIItem : MonoBehaviour
    {
        private Score _score;
        private Text _text;
        
        private const string ScoreKey = "score";
        
        /// <summary>
        /// Score items initialization.
        /// </summary>
        public void Init()
        {            
            L10n.LanguageLoaded += OnLanguageChange;            
            _text = GetComponentInChildren<Text>();
            _score = GameManager.Instance.score;
            _score.ScoreChanged += OnScoreChange;

            SetText(_score.CurrentScore);
        }

        /// <summary>
        /// Updates score text on screen when score amount changes.
        /// </summary>
        /// <param name="score">Current score</param>
        private void OnScoreChange(int score)
        {
            SetText(score);
        }

        /// <summary>
        /// Updates score text on screen when language is changed.
        /// </summary>
        /// <param name="currentLang">Current language code</param>
        private void OnLanguageChange(LangCode currentLang)
        {
            SetText(_score.CurrentScore);
        }

        /// <summary>
        /// Sets score text.
        /// </summary>
        /// <param name="score">Current score</param>
        private void SetText(int score)
        {
            string translation = L10n.CurrentLanguage.GetTranslation(ScoreKey);            
            _text.text = string.Format(translation, score, _score.TargetScore);
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
            _score.ScoreChanged -= OnScoreChange;
            L10n.LanguageLoaded -= OnLanguageChange;
        }
    }
}