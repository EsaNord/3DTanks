using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tanks3D.Editor
{
    [CustomEditor(typeof(EnemyUnit))]
    public class EnemyUnitInspector : UnityEditor.Editor
    {
        private EnemyUnit m_euTarget;
        private int m_iDamageAmount = 10;

        private void OnEnable()
        {
            m_euTarget = target as EnemyUnit;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;
            GUILayout.Label("Provide damage to the unit",EditorStyles.boldLabel);
            m_iDamageAmount = EditorGUILayout.IntField("Damage amount", m_iDamageAmount);

            if (GUILayout.Button(string.Format("Take {0} damage", m_iDamageAmount)))
            {
                m_euTarget.TakeDamage(m_iDamageAmount);
            }
        }
    }
}