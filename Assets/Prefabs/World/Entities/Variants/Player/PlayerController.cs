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
        base.Interact();
        player.SelectedObject?.onInteracted?.Invoke();
    }
}