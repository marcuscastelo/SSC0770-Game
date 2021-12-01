using UnityEngine;
using UnityEngine.Assertions;

using Hypnos.Core;

public class ApplyBuffToInteractor : MonoBehaviour, IInteractionResponse
{
    [Header("Config")]
    public Buff buff;

    public DialogInfo confirmationDialogInfo = null;
    public DialogInfo alreadyHasBuffDialogInfo = null;

    void Awake()
    {
        if (confirmationDialogInfo == null)
        {
            Debug.LogWarning("ApplyBuffToInteractor: No confirmation dialog info set, using default.");
            confirmationDialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
            confirmationDialogInfo.title = "Select Buff";
            confirmationDialogInfo.content = "Do you want to select " + buff.ToString() + " as a buff?";
            confirmationDialogInfo.buttons = DialogButtonCombination.YesNo;
        }

        if (alreadyHasBuffDialogInfo == null)
        {
            Debug.LogWarning("ApplyBuffToInteractor: No already has buff dialog info set, using default.");
            alreadyHasBuffDialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
            alreadyHasBuffDialogInfo.title = "Already has buff";
            alreadyHasBuffDialogInfo.content = "You already have " + buff.ToString() + " as a buff.";
            alreadyHasBuffDialogInfo.buttons = DialogButtonCombination.OK;
        }

        if (confirmationDialogInfo.buttons != DialogButtonCombination.YesNo)
        {
            Debug.LogError("ApplyBuffToInteractor: Confirmation dialog info has wrong button combination, overwriting to YesNo.");
            confirmationDialogInfo.buttons = DialogButtonCombination.YesNo;
        }

        if (alreadyHasBuffDialogInfo.buttons != DialogButtonCombination.OK)
        {
            Debug.LogError("ApplyBuffToInteractor: Already has buff dialog info has wrong button combination, overwriting to OK.");
            alreadyHasBuffDialogInfo.buttons = DialogButtonCombination.OK;
        }
    }

    public void OnInteracted(Interaction interaction)
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
            DialogSystem.ShowDialog(new Dialog(alreadyHasBuffDialogInfo, _ => { interaction.EndInteraction(false); }));
            return;
        }

        Dialog buffConfirmationDialog = new Dialog(confirmationDialogInfo, (DialogButton pressedButton) =>
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