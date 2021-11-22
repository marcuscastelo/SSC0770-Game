using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISimpleTargetPlayer : MonoBehaviour
{
    public EntityController controller;
    public Transform self;
    public Transform targetPlayer;

    void Awake()
    {
        Debug.Assert(controller != null, "AISimpleTargetPlayer.Awake() - controller is null");
        Debug.Assert(self != null, "AISimpleTargetPlayer.Awake() - self is null");
        Debug.Assert(targetPlayer != null, "AISimpleTargetPlayer.Awake() - targetPlayer is null");
    }

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
