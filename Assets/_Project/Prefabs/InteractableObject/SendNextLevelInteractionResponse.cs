using UnityEngine;

using Hypnos.Core;
using Zenject;

public class SendNextLevelInteractionResponse : MonoBehaviour, IInteractionResponse
{
    [SerializeField] private int nextLevel;

    private LevelSwitcher _levelSwitcher;

    [Inject]
    public void Construct(LevelSwitcher levelSwitcher)
    {
        _levelSwitcher = levelSwitcher;
    }

    public void OnInteracted(Interaction interaction)
    {
        _levelSwitcher.SwitchToLevel(nextLevel);
        interaction.EndInteraction(true);
    }
}