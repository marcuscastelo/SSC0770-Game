using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnSelected();
    void OnDeselected();
    void OnInteract(IInteractor interactor);
}
