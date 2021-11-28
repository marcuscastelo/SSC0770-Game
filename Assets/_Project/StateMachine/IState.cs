using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    public event System.Action OnEntered = delegate { };
    public event System.Action<float> OnExecuted = delegate { };
    public event System.Action OnExited = delegate { };

    protected abstract void EnterBehaviour();
    protected abstract void ExecuteBehaviour(float deltaTime);
    protected abstract void ExitBehaviour();

    bool _isActive = false;

    public void Enter()
    {
        if (_isActive)
            return;

        _isActive = true;

        EnterBehaviour();
        OnEntered();
    }

    public void Execute(float deltaTime)
    {
        if (!_isActive)
            return;

        ExecuteBehaviour(deltaTime);
        OnExecuted(deltaTime);
    }

    public void Exit()
    {
        if (!_isActive)
            return;

        _isActive = false;
        
        ExitBehaviour();
        OnExited();
    }

}
