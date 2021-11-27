using UnityEngine;
using UnityEditor;

namespace Hypnos.Entities
{
    [ExecuteInEditMode]
    public class EntityGizmos : MonoBehaviour
    {
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private Entity entity;

        private SpriteRenderer _spriteRenderer = null;

        private float Height => (_spriteRenderer != null) ? (_spriteRenderer.sprite.texture.height / _spriteRenderer.sprite.pixelsPerUnit) : 0;

        [ContextMenu("Find Entity")]
        void Awake()
        {
            entity = gameObject.GetComponentInParent<Entity>();
        }

        void Update()
        {
            if (Application.isPlaying)
                return;

            Start();
        }

        void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (!showGizmos)
                return;

            Vector2 headPos = entity.transform.position + Vector3.up * Height;

            // Draw text: entity health
            Handles.Label(headPos, $"{entity.name}: ({entity.Health.CurrentHealth})");

        }
#endif
    }
}