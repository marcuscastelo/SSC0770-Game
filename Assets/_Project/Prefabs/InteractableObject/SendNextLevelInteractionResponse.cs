using UnityEngine;
using UnityEngine.Assertions;

using Hypnos.Audio;
using Hypnos.Core;
using Zenject;

public class SendNextLevelInteractionResponse : MonoBehaviour, IInteractionResponse
{
    [SerializeField] private int nextLevel;
    [SerializeField] private DialogInfo confirmationDialogInfo;

    private LevelSwitcher _levelSwitcher;

    [Inject]
    public void Construct(LevelSwitcher levelSwitcher)
    {
        _levelSwitcher = levelSwitcher;
    }

    public void OnValidate()
    {
        Assert.IsTrue(nextLevel > 0);
        if (confirmationDialogInfo != null)
            Assert.AreEqual(confirmationDialogInfo.buttons, DialogButtonCombination.YesNo, "Only Yes/No buttons are supported for this interaction response");

    }

    void Awake()
    {
        if (confirmationDialogInfo == null)
        {
            confirmationDialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
            confirmationDialogInfo.title = "Go to next level?";
            confirmationDialogInfo.content = "Are you sure you want to go to the next level?";
            confirmationDialogInfo.buttons = DialogButtonCombination.YesNo;
        }
    }

    public void OnInteracted(Interaction interaction)
    {
        Dialog dialog = new Dialog(confirmationDialogInfo, (pressedButton) =>
        {
            if (pressedButton == DialogButton.Yes)
            {
                _levelSwitcher.SwitchToLevel(nextLevel);
                interaction.EndInteraction(true);
            }
            else
            {
                interaction.EndInteraction(false);
            }
        });

        DialogSystem.ShowDialog(dialog);
    }
}