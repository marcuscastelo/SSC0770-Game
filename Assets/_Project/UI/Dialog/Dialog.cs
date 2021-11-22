public class Dialog
{
    public delegate void DialogCallback(DialogButton button);

    public DialogInfo dialogInfo;
    public DialogCallback callback;

    public Dialog(DialogInfo dialogInfo, DialogCallback callback)
    {
        this.dialogInfo = dialogInfo;
        this.callback = callback;
    }

    public void OnButtonPressed(DialogButton button) => callback(button);

    public static DialogButton IndexToButton(DialogButtonCombination comb, int index) 
    {
        switch (comb)
        {
            case DialogButtonCombination.OK:
                return DialogButton.OK;
            case DialogButtonCombination.OKCancel:
                return index == 1 ? DialogButton.OK : DialogButton.Cancel;
            case DialogButtonCombination.YesNo:
                return index == 1 ? DialogButton.Yes : DialogButton.No;
            case DialogButtonCombination.YesNoCancel:
                return index == 1 ? DialogButton.Yes : index == 2 ? DialogButton.No : DialogButton.Cancel;
            default:
                throw new System.Exception("Invalid DialogButtonCombination: " + comb);
        }
    }

    public static int GetButtonCount(DialogButtonCombination comb)
    {
        switch (comb)
        {
            case DialogButtonCombination.OK:
                return 1;
            case DialogButtonCombination.OKCancel:
                return 2;
            case DialogButtonCombination.YesNo:
                return 2;
            case DialogButtonCombination.YesNoCancel:
                return 3;
            default:
                throw new System.Exception("Invalid DialogButtonCombination: " + comb);
        }
    }

    public static string ButtonToString(DialogButton button)
    {
        switch (button)
        {
            case DialogButton.OK:
                return "OK";
            case DialogButton.Cancel:
                return "Cancel";
            case DialogButton.Yes:
                return "Yes";
            case DialogButton.No:
                return "No";
            default:
                throw new System.Exception("Invalid DialogButton: " + button);
        }
    }
}