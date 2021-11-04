using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfo : ScriptableObject
{
    public string title = "";
    public string content = "";
    public bool hasCancel = true;

    public DialogInfo()
    {
        title = "Dialog not found";
        content = "Unavailable";
        hasCancel = true;
    }
}
