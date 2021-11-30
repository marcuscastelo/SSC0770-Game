using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Entities;


public class TroubleMakerAI : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private Entity _selfEntity;
    [SerializeField] private Entity _targetEntity;


    private Vector2 _moveDir;
    private Vector2 _vecToTarget;

    void Start() => StartCoroutine(AILoop());

    private IEnumerator AILoop()
    {
        while (_selfEntity.Health.CurrentHealth > 0)
        {
            while (!_selfEntity.SpriteRenderer.isVisible)
            {
                _selfEntity.Controller.Move(Vector2.zero);
                yield return null;
            }

            UpdateVectors();
            float distance = _vecToTarget.magnitude;

            if (distance > 1f)
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
        _vecToTarget = (_targetEntity.transform.position - _selfEntity.transform.position);

        Vector2 pushVec = Vector2.zero;

        Entity[] otherEntities = GameObject.FindObjectsOfType<Entity>();
        foreach (Entity entity in otherEntities)
        {
            if (entity.gameObject != _selfEntity.gameObject && entity.gameObject != _targetEntity.gameObject)
            {
                Vector2 vecOutOfEntity = entity.transform.position - _selfEntity.transform.position;
                pushVec += vecOutOfEntity.normalized * Mathf.Max(0.1f, 1/(vecOutOfEntity.sqrMagnitude+0.01f)) * pushForce;
            }
        }
        
        _moveDir = _vecToTarget + pushVec;
        _moveDir = _moveDir.normalized;
    }
}
