using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState<Stats, RTI> : IState where Stats: EntityStats where RTI: EntityRTI
{
    public readonly EntityData<Stats, RTI> playerData;

    public EntityState(PlayerData data)
    {
        this.playerData = data;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
