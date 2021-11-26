using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Core
{
    public interface IInteractable
    {
        void OnSelected();
        void OnDeselected();
        void OnInteract(Interaction interaction);
    }
}