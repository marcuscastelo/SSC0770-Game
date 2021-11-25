using UnityEngine;

public class ApplyBuffToInteractor : MonoBehaviour
{
    [Header("Config")]
    public Buff buff;

    public void ApplyBuffTo(IInteractor target)
    {
        if (!(target is IBuffable))
            return;

        IBuffable buffable = target as IBuffable;

        DialogInfo buffConfirmationDialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
        buffConfirmationDialogInfo.title = "Select Buff";
        buffConfirmationDialogInfo.content = "Do you want to select " + buff.ToString() + " as a buff?";
        buffConfirmationDialogInfo.buttons = DialogButtonCombination.YesNo;
        Dialog buffConfirmationDialog = new Dialog(buffConfirmationDialogInfo, (DialogButton pressedButton) =>
        {
            if (pressedButton == DialogButton.Yes)
            {
                buffable.ApplyBuff(buff);
            }
        });

        DialogSystem.ShowDialog(buffConfirmationDialog);

    }
}