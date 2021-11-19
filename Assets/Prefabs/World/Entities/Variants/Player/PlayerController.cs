using UnityEngine;

public class PlayerController : EntityController 
{
    [SerializeField]
    private Player player;

    void Awake()
    {
        if (player == null)
            player = GetComponent<Player>();
    }

    public override void Interact()
    {
        if (player.SelectedObject == null)
        {
            Debug.LogWarning("PlayerController.Interact() - No selected object");
            return;
        }

        DialogInfo buffConfirmationDialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
        buffConfirmationDialogInfo.title = "Select Buff";
        buffConfirmationDialogInfo.content = "Do you want to select " + player.SelectedObject.name + " as a buff?";
        buffConfirmationDialogInfo.buttons = DialogButtonCombination.YesNo;
        Dialog buffConfirmationDialog = new Dialog(buffConfirmationDialogInfo, (DialogButton pressedButton) => {
            if (pressedButton == DialogButton.Yes)
            {
                player.SelectedObject.OnInteractedBy(player);
                player.ActiveBuff = Buff.Armor;
            }
        });

        DialogSystem.ShowDialog(buffConfirmationDialog);
    }
}