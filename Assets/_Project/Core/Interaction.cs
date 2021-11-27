using System.ComponentModel;
using UnityEngine;

namespace Hypnos.Core
{
    public class Interaction
    {
        public readonly IInteractor Interactor;
        public readonly IInteractable Interactable;

        public delegate void OnInteractionStarted();
        public event OnInteractionStarted OnInteractionStartedEvent;

        public delegate void OnInteractionEnded(bool success);
        public event OnInteractionEnded OnInteractionEndedEvent;

        private bool _started = false;
        private bool _ended = false;

        public Interaction(IInteractor interactor, IInteractable interactable)
        {
            Interactor = interactor;
            Interactable = interactable;
        }

        public void StartInteraction()
        {
            if (_started)
            {
                Debug.LogWarning("Interaction already started");
                return;
            }

            _started = true;
            OnInteractionStartedEvent?.Invoke();
        }

        public void EndInteraction(bool success) {
            if (_ended)
            {
                Debug.LogWarning("Interaction already ended");
                return;
            }

            _ended = true;
            OnInteractionEndedEvent?.Invoke(success);
        }
    }
}