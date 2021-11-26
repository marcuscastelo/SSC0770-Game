using UnityEngine;
using UnityEngine.Assertions;

public class ApplyBuffToInteractor : MonoBehaviour
{
    [Header("Config")]
    public Buff buff;

    public DialogInfo dialogInfo = null;

    void Awake()
    {
        if (dialogInfo == null)
        {
            dialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
            dialogInfo.title = "Select Buff";
            dialogInfo.content = "Do you want to select " + buff.ToString() + " as a buff?";
            dialogInfo.buttons = DialogButtonCombination.YesNo;
        }
    }

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

        if (buffable.HasBuff(buff))
        {
            DialogInfo buffAlreadyActive = new DialogInfo()
            {
                title = "Buff already active",
                content = $"{buff} is already active",
                buttons = DialogButtonCombination.OK
            };
            DialogSystem.ShowDialog(buffAlreadyActive);
            return;
        }

        Dialog buffConfirmationDialog = new Dialog(dialogInfo, (DialogButton pressedButton) =>
        {
            if (pressedButton == DialogButton.Yes)
            {
                buffable.ApplyBuff(buff);
            }
        });

        DialogSystem.ShowDialog(buffConfirmationDialog);
    }
}