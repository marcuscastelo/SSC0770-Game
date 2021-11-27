using UnityEngine;
using UnityEditor;

using Hypnos.Entities;
using Hypnos.Core;
using Zenject;

public class BuffDisplay : MonoBehaviour
{
    private IBuffable _buffableEntity;
    private Animator _animator;

    [SerializeField] private RuntimeAnimatorController armorAnimatorController;
    [SerializeField] private RuntimeAnimatorController noItemAnimatorController;
    [SerializeField] private RuntimeAnimatorController swordAnimatorController;
    [SerializeField] private RuntimeAnimatorController swordAndArmorAnimatorController;
    private Buff _lastBuff;

    [Inject]
    public void Construct(Entity entity, Animator animator)
    {
        _buffableEntity = entity;
        _animator = animator;
        _lastBuff = _buffableEntity.ActiveBuff;
    }

    void Start() => ChangeAnimatorController(_lastBuff);

    private void ChangeAnimatorController(Buff buffs)
    {
        if (buffs.HasFlag(Buff.Armor | Buff.Sword))
            _animator.runtimeAnimatorController = swordAndArmorAnimatorController;
        else if (buffs.HasFlag(Buff.Armor))
            _animator.runtimeAnimatorController = armorAnimatorController;
        else if (buffs.HasFlag(Buff.Sword))
            _animator.runtimeAnimatorController = swordAnimatorController;
        else
            _animator.runtimeAnimatorController = noItemAnimatorController;
    }

    void Update()
    {
        if (_buffableEntity == null) return;

        if (_lastBuff != _buffableEntity.ActiveBuff)
        {
            ChangeAnimatorController(_buffableEntity.ActiveBuff);
            _lastBuff = _buffableEntity.ActiveBuff;
        }
    }
}