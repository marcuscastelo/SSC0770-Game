using System.Collections;
using System;

[Serializable]
public class EntityStats
{
   CombatStats combatStats;
   WalkStats walkStats;
   DashStats dashStats;
}

[Serializable]
public class CombatStats
{
    public float attackDamage;
    public float attackCooldown;
    public float attackDuration;
}

[Serializable]
public class WalkStats
{
    public float acceleration;
    public float maxSpeed;
    public float deceleration;
}

[Serializable]
public class DashStats
{
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
}