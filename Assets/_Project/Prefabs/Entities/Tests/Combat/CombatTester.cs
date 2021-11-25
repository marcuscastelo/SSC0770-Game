using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Entities.Systems;
using Hypnos.Entities;

public class CombatTester : MonoBehaviour
{
    [SerializeField] private EntityController controller;
    [SerializeField] private EntityCombat combat;

    public void Combat_Attack()
    {
        combat.Attack();
    }
    
    public void Controller_Attack()
    {
        controller.Attack();
    }
}
