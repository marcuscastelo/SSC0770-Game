using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogTester))]
public class DialogTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DialogTester myScript = (DialogTester)target;

        DialogInfo dialogInfo = new DialogInfo();
        dialogInfo.title = myScript.Title;
        dialogInfo.content = myScript.Content;
        dialogInfo.buttons = myScript.buttonCombination;

        Dialog dialog = new Dialog(dialogInfo, (DialogButton pb) => myScript.pressedButton = pb);

        if (GUILayout.Button("Test Dialog"))
        {
            DialogSystem.ShowDialog(dialog);
        }
    }
}
