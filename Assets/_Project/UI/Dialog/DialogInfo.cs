using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogInfo", menuName = "ScriptableObjects/DialogInfo")]
public class DialogInfo : ScriptableObject
{
    [TextArea]
    public string title = "";
    [TextArea]
    public string content = "";

    public DialogButtonCombination buttons = DialogButtonCombination.OK;
}
