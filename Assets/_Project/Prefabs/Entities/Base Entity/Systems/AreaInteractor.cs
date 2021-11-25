using UnityEngine;

using System.Collections.Generic;

namespace Hypnos.Entities.Systems
{
    public class AreaInteractor : MonoBehaviour
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
                SelectedInteractable?.OnDeselected();
                collidingInteractables.Add(interactable);
                SelectedInteractable?.OnSelected();
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
                bool exitingIsSelected = interactable == SelectedInteractable;
                collidingInteractables.Remove(interactable);

                if (exitingIsSelected)
                {
                    interactable.OnDeselected();
                    SelectedInteractable?.OnSelected();
                }
            
                Debug.Log("AreaInteractor: IT IS INTERACTABLE");
            }
            else
            {
                Debug.Log("AreaInteractor: NOT INTERACTABLE");
            }
        }

        public void Interact()
        {
            IInteractor interactor = GetComponentInParent<IInteractor>();
            SelectedInteractable?.OnInteract(interactor);
        }
    }
}