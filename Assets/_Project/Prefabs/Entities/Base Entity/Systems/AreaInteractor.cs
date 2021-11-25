using UnityEngine;

namespace Hypnos.Entities.Systems
{
    public class AreaInteractor : MonoBehaviour, IInteractor
    {
        public IInteractable SelectedInteractable { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            IInteractable interactable = collisionObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                SelectedInteractable = interactable;
                SelectedInteractable.OnSelected();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            IInteractable interactable = collisionObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                SelectedInteractable = null;
                interactable.OnDeselected();
            }
        }

        public void Interact()
        {
            SelectedInteractable?.OnInteract(this);
        }
    }
}