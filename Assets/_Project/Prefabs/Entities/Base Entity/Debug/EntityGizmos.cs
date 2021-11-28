#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

using Zenject;

namespace Hypnos.Entities
{
    public class EntityGizmos : MonoBehaviour
    {
        [SerializeField] private bool showGizmos = true;
        [Inject] private Entity _entity;

        private float Height => (_entity.SpriteRenderer != null) ? (_entity.SpriteRenderer.sprite.texture.height / _entity.SpriteRenderer.sprite.pixelsPerUnit) : 0;

        void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Vector2 headPos = _entity.transform.position + Vector3.up * Height;

            // Draw text: entity health
            Handles.Label(headPos, $"{_entity.name}: ({_entity.Health.CurrentHealth})");

        }
    }
}
#endif