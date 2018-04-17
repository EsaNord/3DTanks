using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tanks3D.Localization;
using L10n = Tanks3D.Localization.Localization;

namespace Tanks3D.UI
{
    public class LocalizedLabel : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private string _key;

        private void Awake()
        {
            L10n.LanguageLoaded += OnLanguageLoaded;
        }

        private void Start()
        {
            OnLanguageLoaded(L10n.CurrentLanguage.LanguageCode);
        }

        private void OnLanguageLoaded(LangCode currentLanguage)
        {
            _text.text = L10n.CurrentLanguage.GetTranslation(_key);
        }
    }
}