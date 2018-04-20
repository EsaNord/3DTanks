using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks3D.UI
{
    public class UI : MonoBehaviour
    {
        public HealthUI HealthUI { get; private set; }
        public ScoreUI ScoreUI { get; private set; }
        public LivesUI LivesUI { get; private set; }
        public EndUI EndUI { get; private set; }

        public static UI Current { get; private set; }

        public void Init()
        {
            Current = this;
            HealthUI = GetComponentInChildren<HealthUI>();
            HealthUI.Init();
            ScoreUI = GetComponentInChildren<ScoreUI>();
            ScoreUI.Init();
            LivesUI = GetComponentInChildren<LivesUI>();
            LivesUI.Init();
            EndUI = GetComponentInChildren<EndUI>();
            EndUI.Init();
        }

        /// <summary>
        /// Resets game if button is pressed.
        /// </summary>
        public void Yes()
        {            
            GameManager.Instance.score.CurrentScore = 0;
            GameManager.Instance.Player.ResetUnit();
            List<Unit> enemies = GameManager.Instance.Enemies;

            foreach (Unit unit in enemies)
            {
                unit.ResetUnit();
            }

            EndUI.InitItems();
            Time.timeScale = 1;
        }

        /// <summary>
        /// Quits game if button is pressed.
        /// </summary>
        public void No()
        {
            Application.Quit();
        }
    }
}