using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tanks3D.persistance;
using Tanks3D.Messaging;
using Tanks3D.Localization;
using L10n = Tanks3D.Localization.Localization;
using System;

namespace Tanks3D
{
    public class GameManager : MonoBehaviour
    {
        #region Static

        private static GameManager m_gmInstance;

        public static GameManager Instance
        {
            get
            {
                if (m_gmInstance == null && !IsClosing)
                {
                    GameObject gameManagerObject = new GameObject(typeof(GameManager).Name);
                    m_gmInstance = gameManagerObject.AddComponent<GameManager>();
                }
                return m_gmInstance;
            }
        }

        public static bool IsClosing { get; private set; }

        #endregion

        private List<Unit> _enemyUnits = new List<Unit>();
        private Unit _playerUnit;
        private SaveSystem _saveSystem;

        public Score score;          

        private const string LanguageKey = "Language";

        public string SavePath { get { return Path.Combine(Application.persistentDataPath, "save"); } }

        public MessageBus MessageBus { get; private set; }           

        protected void Awake()
        {
            if (m_gmInstance == null)
            {
                m_gmInstance = this;
            }
            else if (m_gmInstance != this)
            {
                Destroy(gameObject);
                return;
            }
            Init();
        }

        private void OnApplicationQuit()
        {
            IsClosing = true;
        }

        private void OnDestroy()
        {
            L10n.LanguageLoaded -= OnLanguageLoaded;
        }

        private void Init()
        {
            InitLocalization();
            IsClosing = false;            
            MessageBus = new MessageBus();

            score = GetComponent<Score>();
            var UI = FindObjectOfType<UI.UI>();            
            UI.Init();

            UI.ScoreUI.SetScoreItem();            

            Unit[] allUnits = FindObjectsOfType<Unit>();
            foreach (Unit unit in allUnits)
            {
                AddUnit(unit);
            }            

            _saveSystem = new SaveSystem(new BinaryPersistance(SavePath));
            //_saveSystem = new SaveSystem(new JSONPersistence(SavePath));           
        }

        protected void Update()
        {  
            bool save = Input.GetKeyDown(KeyCode.F2);
            bool load = Input.GetKeyDown(KeyCode.F3);

            if (save)
                Save();
            else if (load)
                Load();  
            
            if (score.CurrentScore >= score.TargetScore)
            {
                PlayerWon();
            }            
        }

        /// <summary>
        /// Defeat method,
        /// this method is called when player dies.
        /// </summary>
        public void PlayerLost()
        {
            Debug.Log("Defeat");
        }

        /// <summary>
        /// Victory method,
        /// this method is called when player
        /// collects enough points.
        /// </summary>
        private void PlayerWon()
        {
            Debug.Log("Victory");
        }

        private void AddUnit(Unit unit)
        {
            unit.Init();

            if (unit is EnemyUnit)
            {
                _enemyUnits.Add(unit);
            }
            else if (unit is PlayerUnit)
            {
                _playerUnit = unit;
               UI.UI.Current.LivesUI.SetLivesItem(_playerUnit);
            }
            
            UI.UI.Current.HealthUI.AddUnit(unit);
        }

        public void Save()
        {            
            GameData data = new GameData();
            foreach (Unit unit in _enemyUnits)
            {
                data.EnemyData.Add(unit.GetUnitData());
            }
            data.PlayerData = _playerUnit.GetUnitData();

            _saveSystem.Save(data);
            Debug.Log("Saved;");
        }

        public void Load()
        {
            GameData data = _saveSystem.Load();
            foreach (UnitData unitData in data.EnemyData)
            {                
                Unit enemy = _enemyUnits.FirstOrDefault(unit => unit.Id == unitData.Id);
                if (enemy != null)
                    enemy.SetUnitData(unitData);
            }

            _playerUnit.SetUnitData(data.PlayerData);
        }   
        
        private void InitLocalization()
        {
            LangCode currentLanguage = (LangCode) PlayerPrefs.GetInt(LanguageKey, (int)LangCode.EN);
            L10n.LoadLanguage(currentLanguage);
            L10n.LanguageLoaded += OnLanguageLoaded;
        }

        private void OnLanguageLoaded(LangCode currentLanguage)
        {
            PlayerPrefs.SetInt(LanguageKey, (int)currentLanguage);
        }
    }
}