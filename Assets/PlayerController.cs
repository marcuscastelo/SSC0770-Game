using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EntityMovement entityMovement;

    // Update is called once per frame
    void Update()
    {
        entityMovement.UpdateMovementWill(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
