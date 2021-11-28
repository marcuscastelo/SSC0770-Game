using System;
using System.Collections.Generic;
public static class DialogSystem
{
    private static readonly Queue<Dialog> pendingDialogs = new Queue<Dialog>();
    private static DialogDisplay activeDisplay = null;

    public static void ShowDialog(DialogInfo dialogInfo)
    {
        Dialog dummyDialog = new Dialog(dialogInfo, (DialogButton _)=>{});
        ShowDialog(dummyDialog);
    }

    public static void ShowDialog(Dialog dialog)
    {
        if (activeDisplay == null)
            throw new Exception("No DialogDisplay has been registered!");
        
        if (activeDisplay.IsBusy)
            pendingDialogs.Enqueue(dialog);
        else
            activeDisplay.ShowDialog(dialog);
    }

    public static void ShowNextDialog()
    {
        if (pendingDialogs.Count > 0)
            ShowDialog(pendingDialogs.Dequeue());
    }

    // called by DialogDisplay when a button is pressed
    public static void OnDialogButtonPressed(Dialog dialog, DialogButton button)
    {
        dialog.OnButtonPressed(button);
        ShowNextDialog();
    }

    public static void OnDialogDismissed(Dialog dialog)
    {
        ShowNextDialog();
    }

    public static void RegisterDisplay(DialogDisplay display)
    {
        if (activeDisplay != null)
        {
            throw new Exception("Only one dialog display can be active at a time");
        }
        activeDisplay = display;
        ShowNextDialog();
    }

    public static void UnregisterDisplay(DialogDisplay display)
    {
        if (activeDisplay == display)
        {
            if (activeDisplay.IsBusy) throw new Exception("Cannot unregister an active display");
            activeDisplay = null;
        }
        else
        {
            throw new Exception("Tried to unregister a dialog display that wasn't even regitered (or got overwritten)");
        }
    }

}
