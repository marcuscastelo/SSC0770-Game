using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogDisplay : MonoBehaviour
{
    public GameObject dialogPanel;
    //TextMesh GUI elements references
    //Title, Content, Button1, Button2, Button3

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Content;
    public TextMeshProUGUI Button1Text;
    public TextMeshProUGUI Button2Text;
    public TextMeshProUGUI Button3Text;
    public Button Button1;
    public Button Button2;
    public Button Button3;


    private Dialog _currentDialog;
    public bool IsBusy { get { return _currentDialog != null; } }
    public Dialog CurrentDialog
    {
        get { return _currentDialog; }
        set
        {
            _currentDialog = value;
            dialogPanel.SetActive(_currentDialog != null);
        }
    }

    void Awake()
    {
        DialogSystem.RegisterDisplay(this);
    }

    public void ShowDialog(DialogInfo dialogInfo)
    {
        Dialog dummyDialog = new Dialog(dialogInfo, (DialogButton _) => { });
        ShowDialog(dummyDialog);
    }

    public void ShowDialog(Dialog dialog)
    {
        Debug.Assert(dialog != null, "DialogDisplay.ShowDialog: dialog is null!");
        Debug.Assert(!IsBusy, "DialogDisplay.ShowDialog: already busy!");
        CurrentDialog = dialog;
        AdaptToDialog(dialog);
    }

    public void DismissDialog()
    {
        Debug.Assert(IsBusy, "DialogDisplay.DismissDialog: not busy!");
        Dialog currentDialog = CurrentDialog;
        CurrentDialog = null;
        DialogSystem.OnDialogDismissed(currentDialog);
    }

    private void AdaptToDialog(Dialog dialog)
    {
        Title.text = dialog.dialogInfo.title;
        Content.text = dialog.dialogInfo.content;

        int buttonCount = Dialog.GetButtonCount(dialog.dialogInfo.buttons);
        Button1.gameObject.SetActive(buttonCount >= 1);
        Button2.gameObject.SetActive(buttonCount >= 2);
        Button3.gameObject.SetActive(buttonCount >= 3);

        Button1Text.text = Dialog.ButtonToString(Dialog.IndexToButton(dialog.dialogInfo.buttons, 0));
        Button2Text.text = Dialog.ButtonToString(Dialog.IndexToButton(dialog.dialogInfo.buttons, 1));
        Button3Text.text = Dialog.ButtonToString(Dialog.IndexToButton(dialog.dialogInfo.buttons, 2));
    }

    public void OnDialogButtonPressed(int index)
    {
        Debug.Assert(CurrentDialog != null);

        Dialog currentDialog = this._currentDialog;
        CurrentDialog = null;

        DialogButton pressedButton = Dialog.IndexToButton(currentDialog.dialogInfo.buttons, index);
        DialogSystem.OnDialogButtonPressed(currentDialog, pressedButton);
    }
}
