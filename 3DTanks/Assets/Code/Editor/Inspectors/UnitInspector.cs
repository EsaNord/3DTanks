using UnityEditor;

namespace Tanks3D.Editor
{
    [CustomEditor(typeof(Unit), editorForChildClasses: true)]
    public class UnitInspector : UnityEditor.Editor
    {
        protected virtual void OnEnable()
        {
            Unit unit = target as Unit;

            if (unit != null && unit.Id < 0)
            {
                Undo.RecordObject(unit, "set id for unit");
                unit.RequestId();
            }
        }
    }
}