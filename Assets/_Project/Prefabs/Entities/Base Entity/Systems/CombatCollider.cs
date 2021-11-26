using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Entities.Systems;

[ExecuteAlways]
public class CombatCollider : MonoBehaviour
{
    [SerializeField] private EntityCombat entityCombat;

    enum ColliderType
    {
        Hitbox,
        Hurtbox
    }

    [SerializeField] private ColliderType colliderType;

    [ContextMenu("Find Combat System")]
    void Awake()
    {
        entityCombat = GetComponentInParent<EntityCombat>();
        Debug.Assert(entityCombat != null, "CombatCollider: EntityCombat is null");
        Debug.Assert(GetComponents<Collider2D>().Length == 1, "CombatCollider: There should be only one Collider2D component");
        Debug.Assert(GetComponents<Collider2D>()[0].isTrigger, "CombatCollider: Collider2D component should be a trigger");
        Debug.Assert(GetComponents<Rigidbody2D>().Length == 1, "CombatCollider: There should be only one Rigidbody2D component");
    }

    void Start()
    {
        Debug.Assert(entityCombat != null, "CombatCollider: EntityCombat is null");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (colliderType == ColliderType.Hitbox)
        {
            entityCombat.OnHitboxEnter(other);
        }
        else if (colliderType == ColliderType.Hurtbox)
        {
            entityCombat.OnHurtboxEnter(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (colliderType == ColliderType.Hitbox)
        {
            entityCombat.OnHitboxExit(other);
        }
        else if (colliderType == ColliderType.Hurtbox)
        {
            entityCombat.OnHurtboxExit(other);
        }
    }
}
