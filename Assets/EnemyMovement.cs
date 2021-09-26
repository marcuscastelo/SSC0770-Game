using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Transform player;
    // Update is called once per frame
    void Update()
    {   
        //Sets will to the player's position
        controllerInput = player.position - transform.position;
    }
}
