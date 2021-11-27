namespace Hypnos.Core
{
    public interface IInteractor
    {
        IInteractable SelectedInteractable { get; }
        void Interact();
    }
}