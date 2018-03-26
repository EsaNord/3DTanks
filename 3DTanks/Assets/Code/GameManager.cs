using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tanks3D.persistance;
using Tanks3D.Messaging;
using Tanks3D.Localization;
using L10n = Tanks3D.Localization.Localization;

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

        private List<Unit> m_lEnemyUnits = new List<Unit>();
        private Unit m_uPlayerUnit;
        private SaveSystem _saveSystem;

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

        private void Init()
        {
            IsClosing = false;

            MessageBus = new MessageBus();

            var UI = FindObjectOfType<UI.UI>();
            UI.Init();

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
        }

        private void AddUnit(Unit unit)
        {
            unit.Init();

            if (unit is EnemyUnit)
            {
                m_lEnemyUnits.Add(unit);
            }
            else if (unit is PlayerUnit)
            {
                m_uPlayerUnit = unit;
            }
            
            UI.UI.Current.HealthUI.AddUnit(unit);
        }

        public void Save()
        {            
            GameData data = new GameData();
            foreach (Unit unit in m_lEnemyUnits)
            {
                data.EnemyData.Add(unit.GetUnitData());
            }
            data.PlayerData = m_uPlayerUnit.GetUnitData();

            _saveSystem.Save(data);
            Debug.Log("Saved;");
        }

        public void Load()
        {
            GameData data = _saveSystem.Load();
            foreach (UnitData unitData in data.EnemyData)
            {                
                Unit enemy = m_lEnemyUnits.FirstOrDefault(unit => unit.Id == unitData.Id);
                if (enemy != null)
                    enemy.SetUnitData(unitData);
            }

            m_uPlayerUnit.SetUnitData(data.PlayerData);
        }        
    }
}