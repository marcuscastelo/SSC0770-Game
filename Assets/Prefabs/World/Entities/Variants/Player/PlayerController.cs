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

        player.SelectedObject.Interact(player);
    }
}