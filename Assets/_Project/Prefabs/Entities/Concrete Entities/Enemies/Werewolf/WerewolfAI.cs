using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Entities;

public class WerewolfAI : MonoBehaviour
{
    [SerializeField] private Entity _selfEntity;
    [SerializeField] private Entity _targetEntity;

    private Vector2 _vecToTarget;

    void Start() => StartCoroutine(AILoop());

    private IEnumerator AILoop()
    {
        while (true)
        {
            while (!_selfEntity.SpriteRenderer.isVisible) {
                _selfEntity.Controller.Move(Vector2.zero);
                yield return null;
            }
            
            UpdateVectors();
            float distance = _vecToTarget.magnitude;
            Vector2 direction = _vecToTarget.normalized;

            if (distance > 0.5f) {
                _selfEntity.Controller.Move(direction);
                _selfEntity.Controller.Dash();
            }
            else
            {
                _selfEntity.Controller.Move(Vector2.zero);
                _selfEntity.Controller.LookTo(direction);
                yield return _selfEntity.Controller.AttackCoroutine();
                
                // _selfEntity.Controller.LookTo(-direction);
                // _selfEntity.Controller.Dash(-direction*0.1f);
            }

            yield return null;
        }
    }

    private void UpdateVectors()
    {
        _vecToTarget = _targetEntity.transform.position - _selfEntity.transform.position;
    }
}
