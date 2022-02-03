using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioTester))]
public class AudioTesterEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        AudioTester audioTester = (AudioTester)target;
        if (GUILayout.Button("Test")) {
            audioTester.Test();
        }
    }
}