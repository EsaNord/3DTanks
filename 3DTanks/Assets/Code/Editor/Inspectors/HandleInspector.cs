using UnityEditor;
using UnityEngine;

namespace Tanks3D.Editor
{
    [UnityEditor.CustomEditor(typeof(CollectableSpawner))]
    public class HandleInspector : UnityEditor.Editor
    {
        private CollectableSpawner _targetPM;
        private Transform _handleTransform;
        private Quaternion _handleQuaternion;

        protected void OnEnable()
        {
            _targetPM = target as CollectableSpawner;
            _handleTransform = _targetPM.transform;
        }
        
        /// <summary>
        /// Displays handles in set positions.
        /// </summary>
        private void OnSceneGUI()
        {           
            DisplayLaunchPointHandle(ref _targetPM.Position1);
            DisplayLaunchPointHandle(ref _targetPM.Position2);
        }
        
        /// <summary>
        /// Displays handles in scene and allows them to be moved.
        /// </summary>
        /// <param name="targetPoint">Handles current position in wolrd</param>
        private void DisplayLaunchPointHandle(ref Vector3 targetPoint)
        {            
            _handleQuaternion = Tools.pivotRotation == PivotRotation.Local ?
                _handleTransform.rotation : Quaternion.identity;
            
            Vector3 point = targetPoint;

            EditorGUI.BeginChangeCheck();

            point = Handles.DoPositionHandle(point, _handleQuaternion);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_targetPM, "MoveHandle");
                targetPoint =
                    new Vector3(point.x, 0.5f, point.z);
            }
        }
    }
}