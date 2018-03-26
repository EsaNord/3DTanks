using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Tanks3D.Localization;
using L10n = Tanks3D.Localization.Localization;

namespace Tanks3D.Editor
{
    public class LocalizationWindow : EditorWindow
    {
        [MenuItem("Localization/Edit")]
        private static void OpenWindow()
        {
            LocalizationWindow window = GetWindow<LocalizationWindow>();
            window.Show();           
        }

        private const string LocalizationKey = "Localization";

        public LangCode CurrentLanguage = LangCode.NA;

        private Dictionary<string, string> _localizations =
            new Dictionary<string, string>();
        
        private void OnEnable()
        {
            LangCode languag = (LangCode)EditorPrefs.GetInt(LocalizationKey, (int)LangCode.NA);
        }

        private void SetLanguage(LangCode langCode)
        {
            if (CurrentLanguage == langCode)
                return;

            CurrentLanguage = langCode;
            EditorPrefs.SetInt(LocalizationKey, (int)CurrentLanguage);
            _localizations.Clear();
                      
            L10n.LoadLanguage(CurrentLanguage);
            _localizations = L10n.CurrentLanguage.GetValues();
        }

        private void OnGUI()
        {
            LangCode langCode = (LangCode)EditorGUILayout.EnumPopup(CurrentLanguage);
            SetLanguage(langCode);

            EditorGUILayout.BeginVertical();

            Dictionary<string, string> newValues = new Dictionary<string, string>();
            List<string> deletedKeys = new List<string>();

            foreach ( var localization in _localizations)
            {
                EditorGUILayout.BeginHorizontal();
                string key = EditorGUILayout.TextField(localization.Key);
                string value = EditorGUILayout.TextField(localization.Value);

                newValues.Add(key, value);

                if (GUILayout.Button("X"))
                {
                    deletedKeys.Add(localization.Key);
                }

                EditorGUILayout.EndHorizontal();
            }

            _localizations = newValues;

            foreach ( var deletedKey in deletedKeys)
            {
                if (_localizations.ContainsKey(deletedKey))
                {
                    _localizations.Remove(deletedKey);
                }
            }

            if (GUILayout.Button("Add value"))
            {
                if (!_localizations.ContainsKey(""))
                {
                    _localizations.Add("", "");
                }
            }

            if (GUILayout.Button("Save"))
            {
                L10n.CurrentLanguage.SetValues(_localizations);
                L10n.SaveCurrentLanguage();
            }

            EditorGUILayout.EndVertical();
        }
    }
}