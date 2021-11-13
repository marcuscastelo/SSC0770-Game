using UnityEngine;

public class PlayerController : EntityController 
{
    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Interact() => player.SelectedObject?.onInteracted?.Invoke();
}