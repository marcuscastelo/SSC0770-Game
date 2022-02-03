using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> where T : IState
{
    public delegate S StateSupplier<S>() where S : T;

    protected T currentState;
    public T CurrentState
    {
        get { return currentState; }
    }

    public abstract T CreateInitialState();

    public void ChangeState<S>(StateSupplier<S> supplier) where S : T
    {
        if (currentState is S)
            return;

        if (currentState != null)
            currentState.Exit();

        currentState = supplier();
        currentState.Enter();
    }

    public void Tick(float deltaTime) {
        if (currentState != null)
            currentState.Execute(deltaTime);
    }
}
