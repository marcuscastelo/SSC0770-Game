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

            if (interactable != null)
                Select(interactable);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            IInteractable interactable = collisionObject.GetComponent<IInteractable>();

            if (interactable != null)
                Deselect(interactable);
        }

        private void Select(IInteractable interactable)
        {
            SelectedInteractable?.OnDeselected();
            collidingInteractables.Add(interactable);
            SelectedInteractable?.OnSelected();
        }

        private void Deselect(IInteractable interactable)
        {
            if (interactable == null) return;

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