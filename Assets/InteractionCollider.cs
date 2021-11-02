using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{
    GameObject lastCollidedObject;
    //TODO: update InteractionController selectedObject to be the object that is being interacted with

    //OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        //If it is a selectable object (have a Interact tag), set it as the selected object
    }

}
