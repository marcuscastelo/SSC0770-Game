using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Entities;


public class TroubleMakerAI : MonoBehaviour 
{
    [SerializeField] private Entity _selfEntity;
    [SerializeField] private Entity _targetEntity;

    private Vector2 _vecToTarget;

    void Start() => StartCoroutine(AILoop());

    private IEnumerator AILoop()
    {
        while (_selfEntity.Health.CurrentHealth > 0)
        {
            float distance = _vecToTarget.magnitude;
            Vector2 direction = _vecToTarget.normalized;

            if (distance > 1f) {
                _selfEntity.Controller.Move(direction);
                _selfEntity.Controller.Dash();
            }
            else
            {
                _selfEntity.Controller.Move(Vector2.zero);
                _selfEntity.Controller.LookTo(direction);
                yield return _selfEntity.Controller.AttackCoroutine();
                
                // controller.LookTo(-direction);
                // controller.Dash(-direction*0.1f);
            }

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        _vecToTarget = _targetEntity.transform.position - _selfEntity.transform.position;
    }
}
