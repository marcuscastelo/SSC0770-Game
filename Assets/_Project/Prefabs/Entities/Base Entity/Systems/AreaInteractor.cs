using UnityEngine;

using System.Collections.Generic;

using Hypnos.Core;

namespace Hypnos.Entities.Systems
{
    public class AreaInteractor : MonoBehaviour, IInteractor
    {
        private readonly List<IInteractable> collidingInteractables = new List<IInteractable>();

        public IInteractable SelectedInteractable
        {
            get
            {
                if (collidingInteractables.Count > 0)
                    return collidingInteractables[0];
                else
                    return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            IInteractable interactable = collisionObject.GetComponent<IInteractable>();

            Debug.Log("AreaInteractor: OnTriggerEnter2D: " + collisionObject.name);
            if (interactable != null)
            {
                Debug.Log("AreaInteractor: IT IS INTERACTABLE");
                Select(interactable);
            }
            else
            {
                Debug.Log("AreaInteractor: NOT INTERACTABLE");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            IInteractable interactable = collisionObject.GetComponent<IInteractable>();

            Debug.Log("AreaInteractor: OnTriggerExit2D: " + collisionObject.name);

            if (interactable != null)
            {
                Debug.Log("AreaInteractor: IT IS INTERACTABLE");
                Deselect(interactable);
            }
            else
            {
                Debug.Log("AreaInteractor: NOT INTERACTABLE");
            }
        }

        private void Select(IInteractable interactable)
        {
            SelectedInteractable?.OnDeselected();
            collidingInteractables.Add(interactable);
            SelectedInteractable?.OnSelected();
        }

        private void Deselect(IInteractable interactable)
        {
            bool targetIsTop = interactable == SelectedInteractable;
            collidingInteractables.Remove(interactable);

            if (targetIsTop)
            {
                interactable.OnDeselected();
                SelectedInteractable?.OnSelected();
            }
        }

        public void Interact() => Interact((bool success) => { });
        public void Interact(Interaction.OnInteractionEnded onInteractionEnded)
        {
            if (SelectedInteractable == null) {
                onInteractionEnded(false);
                return;
            }

            Interaction interaction = new Interaction(this, SelectedInteractable);

            interaction.OnInteractionEndedEvent += _ => Deselect(SelectedInteractable);
            interaction.OnInteractionEndedEvent += onInteractionEnded;

            SelectedInteractable.OnInteract(interaction);
        }
    }
}