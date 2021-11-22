using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISimpleTargetPlayer : MonoBehaviour
{
    public EntityController controller;
    public Transform self;
    public Transform targetPlayer;

    void Update()
    {
        Vector2 difference = targetPlayer.position - self.position;
        float magnitude = difference.magnitude;
        Vector2 direction = difference.normalized;
        
        if (magnitude > 0.1f)
            controller.Move(direction);
        else
            controller.Move(Vector2.zero);
    }
}
