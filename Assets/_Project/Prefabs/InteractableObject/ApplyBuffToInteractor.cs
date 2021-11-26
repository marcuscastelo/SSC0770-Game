using UnityEngine;
using UnityEngine.Assertions;

public class ApplyBuffToInteractor : MonoBehaviour
{
    [Header("Config")]
    public Buff buff;

    public void ApplyBuffTo(IInteractor target)
    {
        Assert.IsNotNull(target);
        
        IBuffable buffable = target as IBuffable;
        if (buffable == null)
        {
            if (target is MonoBehaviour monoBehaviour)
            {
                buffable = monoBehaviour.gameObject.GetComponentInParent<IBuffable>();
                if (buffable == null)
                {
                    Debug.LogWarning($"{target} is not buffable");
                    return;
                }
            }
        }

        Assert.IsNotNull(buffable);

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