using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CombatTester))]
public class CombatTesterEditor : Editor
{
    SerializedProperty combat;
    SerializedProperty controller;

    void OnEnable()
    {
        combat = serializedObject.FindProperty("combat");
        controller = serializedObject.FindProperty("controller");
    }

    public override void OnInspectorGUI()
    {
        CombatTester combatTester = (CombatTester)target;
        serializedObject.Update();

        EditorGUILayout.PropertyField(combat);
        EditorGUILayout.PropertyField(controller);

        if (GUILayout.Button("Combat Attack"))
            combatTester.Combat_Attack();

        if (GUILayout.Button("Controller Attack"))
            combatTester.Controller_Attack();

        serializedObject.ApplyModifiedProperties();
    }
}
