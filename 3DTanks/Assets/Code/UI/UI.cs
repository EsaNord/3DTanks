using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks3D.UI
{
    public class UI : MonoBehaviour
    {
        public HealthUI HealthUI { get; private set; }

        public static UI Current { get; private set; }

        public void Init()
        {
            Current = this;
            HealthUI = GetComponentInChildren<HealthUI>();
            HealthUI.Init();
        }
    }
}