using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTick : MovementTick
{
    protected override bool CanMove()
    {
        throw new System.Exception("PlayerMovementTick.CanMove() not implemented");
    }
}