using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.UI
{
    public class UI : MonoBehaviour
    {
        public HealthUI HealthUI { get; private set; }
        public ScoreUI ScoreUI { get; private set; }
        public LivesUI LivesUI { get; private set; }

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
        }
    }
}