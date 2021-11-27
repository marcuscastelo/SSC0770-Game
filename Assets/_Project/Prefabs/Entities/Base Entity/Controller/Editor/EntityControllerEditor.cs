using UnityEngine;
using UnityEditor;

namespace Hypnos.Entities
{
    [CustomEditor(typeof(EntityController))]
    public class EntityControllerEditor : Editor
    {
        private bool showDebug = false;
        private Vector2 _testMovement;
#if UNITY_EDITOR
        public override void OnInspectorGUI()
        {
            EntityController controller = (EntityController)target;
            base.OnInspectorGUI();

            showDebug = GUILayout.Toggle(showDebug, "Show Debug");
            if (showDebug)
            {
                EditorGUILayout.Vector2Field("Movement", _testMovement);
                if (GUILayout.Button("Move")) controller.Move(_testMovement);
                if (GUILayout.Button("Attack")) controller.Attack();
                if (GUILayout.Button("Dash")) controller.Dash();
                if (GUILayout.Button("Interact")) controller.Interact();
            }

        }
#endif
    }

}