using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PBStateMachine : StateMachine<PlayerBrainState>
{
    PlayerBrain brain;

    public PBStateMachine(PlayerBrain brain)
    {
        this.brain = brain;
    }

    public void ChangeState<S>() where S : PlayerBrainState
    {
        ChangeState(() => (S)Activator.CreateInstance(typeof(S), new object[]{ brain }));
    }

    public override PlayerBrainState CreateInitialState()
    {
        return new PBWalkState(this.brain);
    }
}
