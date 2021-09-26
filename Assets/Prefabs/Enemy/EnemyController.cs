using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    public Transform player;
    void Update()
    {
        this.movement.MoveWill = player.position - transform.position;
    }
}
