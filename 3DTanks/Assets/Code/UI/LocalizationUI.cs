using System.Collections;
using System.Collections.Generic;
using Tanks3D.Localization;
using UnityEngine;
using L10n = Tanks3D.Localization.Localization;

namespace Tanks3D.UI
{
    public class LocalizationUI : MonoBehaviour
    {
        public void SetEnglish()
        {
            L10n.LoadLanguage(LangCode.EN);
        }

        public void SetFinnish()
        {
            L10n.LoadLanguage(LangCode.FI);
        }
    }
}