using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    public Transform player;
    void Update()
    {
        this.movementWill = player.position - transform.position;
    }
}
