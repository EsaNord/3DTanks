using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_gmInstance;

        public static GameManager Instance
        {
            get
            {
                if (m_gmInstance == null)
                {
                    GameObject gameManagerObject = new GameObject(typeof(GameManager).Name);
                    m_gmInstance = gameManagerObject.AddComponent<GameManager>();
                }
                return m_gmInstance;
            }
        }

        private List<Unit> m_lEnemyUnits = new List<Unit>();
        private Unit m_uPlayerUnit;

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
        }

        private void Init()
        {
            Unit[] allUnits = FindObjectsOfType<Unit>();

            foreach (Unit unit in allUnits)
            {
                AddUnit(unit);
            }
        }

        private void AddUnit(Unit unit)
        {
            if (unit is EnemyUnit)
            {
                m_lEnemyUnits.Add(unit);
            }
            else if (unit is PlayerUnit)
            {
                m_uPlayerUnit = unit;
            }
        }
    }
}