using UnityEngine;
using UnityEditor;

using Zenject;

[CustomEditor(typeof(ClockDisplay))]
public class ClockDisplayEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) PlayModeGUI();
    }

    private void PlayModeGUI() {
        ClockDisplay clockDisplay = (ClockDisplay)target;
        bool isPaused = clockDisplay.Clock.Paused;
        clockDisplay.Clock.Paused = EditorGUILayout.Toggle("Paused", isPaused);
    }
}