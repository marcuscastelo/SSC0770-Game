using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using Hypnos.Entities;

public class WerewolfAI : MonoBehaviour
{
    [SerializeField] private Entity _selfEntity;
    [SerializeField] private Entity _targetEntity;

    private Vector2 _moveDir;
    private Vector2 _vecToTarget;

    private void Awake()
    {
        Assert.IsNotNull(_selfEntity, $"TroubleMakerAI({gameObject}): _selfEntity is null");
        Assert.IsNotNull(_targetEntity, $"TroubleMakerAI({gameObject}): _targetEntity is null");
    }

    void Start() => StartCoroutine(AILoop());

    private IEnumerator AILoop()
    {
        if (_targetEntity == null)
            yield break;

        while (_selfEntity.Health.CurrentHealth > 0)
        {
            while (!_selfEntity.SpriteRenderer.isVisible)
            {
                _selfEntity.Controller.Move(Vector2.zero);
                yield return null;
            }

            UpdateVectors();
            float distance = _vecToTarget.magnitude;

            if (distance > 1.2f || (distance > 0.2f && !_selfEntity.AttackerSystem.CanAttack()))
            {
                _selfEntity.Controller.Move(_moveDir);
                _selfEntity.Controller.Dash();
            }
            else
            {
                _selfEntity.Controller.Move(Vector2.zero);
                _selfEntity.Controller.LookTo(_moveDir);
                yield return _selfEntity.Controller.AttackCoroutine();

                // controller.LookTo(-direction);
                // controller.Dash(-direction*0.1f);
            }

            yield return null;
        }
    }

    private void UpdateVectors()
    {
        _vecToTarget = _targetEntity.transform.position - _selfEntity.transform.position;
        _moveDir = _vecToTarget.normalized;
    }
}
