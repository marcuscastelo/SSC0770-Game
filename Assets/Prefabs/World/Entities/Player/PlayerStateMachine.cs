using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine 
{
    Player player;
    public PlayerStateMachine(Player player)
    {
        this.player = player;
        // ChangeState(new PlayerIdleState(player));
    }
}