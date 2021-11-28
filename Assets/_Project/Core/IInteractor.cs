namespace Hypnos.Core
{
    public interface IInteractor
    {
        IInteractable SelectedInteractable { get; }
        void Interact(Interaction.OnInteractionEnded onInteractionEnded); //TODO: think of a better way to do this
        void Interact();
    }
}