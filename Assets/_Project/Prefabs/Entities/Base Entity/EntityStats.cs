using System.Collections;
using System;

[Serializable]
public class EntityStats
{
   CombatStats combatStats;
   MovementStats movementStats;
}

[Serializable]
public class CombatStats
{
    public float attackDamage;
    public float attackCooldown;
    public float attackDuration;
}

[Serializable]
public class MovementStats
{
    public float acceleration;
    public float maxSpeed;
    public float deceleration;

    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
}