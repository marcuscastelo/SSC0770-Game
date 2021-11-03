using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTick : MonoBehaviour
{
    // public Entity entity;
    public StateMachine stateMachine;

    public void Update()
    {
        stateMachine.Update();
    }
}