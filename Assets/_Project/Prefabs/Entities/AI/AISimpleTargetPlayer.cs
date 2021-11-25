using System.Collections;
using System.Collections.Generic;
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
        Debug.Assert(controller != null, "AISimpleTargetPlayer.Awake() - controller is null");
        Debug.Assert(self != null, "AISimpleTargetPlayer.Awake() - self is null");
        Debug.Assert(targetPlayer != null, "AISimpleTargetPlayer.Awake() - targetPlayer is null");
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
                // controller.Dash();
            }

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        _vecToTarget = targetPlayer.position - self.position;
    }
}
