using UnityEngine;
using UnityEngine.Assertions;

using Hypnos.Core;

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

    public void ApplyBuffTo(Interaction interaction)
    {
        IInteractor target = interaction.Interactor;
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
                    interaction.EndInteraction(false);
                    return;
                }
            }
        }

        Assert.IsNotNull(buffable);

        if (buffable.HasBuff(buff))
        {
            DialogInfo buffAlreadyActiveDI = new DialogInfo() // TODO: make this a scriptable object
            {
                title = "Buff already active",
                content = $"{buff} is already active",
                buttons = DialogButtonCombination.OK
            };
            DialogSystem.ShowDialog(new Dialog(buffAlreadyActiveDI, _ => { interaction.EndInteraction(false); }));
            return;
        }

        if (dialogInfo.buttons != DialogButtonCombination.YesNo)
            Debug.LogWarning($"Buff confirmation dialog is not configured correctly. Please set {nameof(dialogInfo.buttons)} to {nameof(DialogButtonCombination.YesNo)}");

        Dialog buffConfirmationDialog = new Dialog(dialogInfo, (DialogButton pressedButton) =>
        {
            if (pressedButton == DialogButton.Yes)
            {
                buffable.ApplyBuff(buff);
                interaction.EndInteraction(true);
            }
            else
            {
                interaction.EndInteraction(false);
            } 
        });

        DialogSystem.ShowDialog(buffConfirmationDialog);
    }
}