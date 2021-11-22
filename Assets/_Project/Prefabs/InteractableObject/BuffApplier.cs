using UnityEngine;

//<summary>
// Attach this component to a SelectableObject to apply buffs to the player when it is selected.
//</summary>
public class BuffApplier: MonoBehaviour
{
    [Header("References")]
    public SelectableObject selectableObject;

    [Header("Config")]
    public Buff buff;

    public void ApplyBuffTo(Player player)
    {
        player.ActiveBuff |= buff;
    }
}