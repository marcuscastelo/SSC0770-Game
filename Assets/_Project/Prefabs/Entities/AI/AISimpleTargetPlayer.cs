using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

using Hypnos.Entities;

public class AISimpleTargetPlayer : MonoBehaviour
{
    public EntityController controller;
    public Transform self;
    public Transform targetPlayer;

    private Vector2 _vecToTarget;

    void Awake()
    {
        Assert.IsNotNull(controller, "AISimpleTargetPlayer: controller is null");
        Assert.IsNotNull(self, "AISimpleTargetPlayer: self is null");
        Assert.IsNotNull(targetPlayer, "AISimpleTargetPlayer: targetPlayer is null");
    }

    void Start()
    {
        StartCoroutine(ExecuteIA());
    }

    private IEnumerator ExecuteIA()
    {
        while (true)
        {
            float magnitude = _vecToTarget.magnitude;
            Vector2 direction = _vecToTarget.normalized;

            if (magnitude > 1f) {
                controller.Move(direction);
                controller.Dash();
            }
            else
            {
                controller.Move(Vector2.zero);
                controller.LookTo(direction);
                yield return controller.AttackCoroutine();
                
                // controller.LookTo(-direction);
                // controller.Dash(-direction*0.1f);
            }

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        _vecToTarget = targetPlayer.position - self.position;
    }
}
