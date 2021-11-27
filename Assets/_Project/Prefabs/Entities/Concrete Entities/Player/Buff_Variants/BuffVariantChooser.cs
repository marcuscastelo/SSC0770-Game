using UnityEngine;
using UnityEditor;

using Hypnos.Entities;
using Zenject;

[ExecuteInEditMode]
public class BuffVariantChooser : MonoBehaviour
{
    private Entity _entity;

    [SerializeField] private RuntimeAnimatorController armorAnimatorController;
    [SerializeField] private RuntimeAnimatorController noItemAnimatorController;
    [SerializeField] private RuntimeAnimatorController swordAnimatorController;
    [SerializeField] private RuntimeAnimatorController swordAndArmorAnimatorController;

    [Inject]
    public void Construct(Entity entity)
    {
        _entity = entity;
    }

    public void SetVariant(Buff buffs)
    {
        switch (buffs)
        {
            case Buff.Armor:
                _entity.Animator.runtimeAnimatorController = armorAnimatorController;
                break;
            case Buff.NoItem:
                _entity.Animator.runtimeAnimatorController = noItemAnimatorController;
                break;
            case Buff.Sword:
                _entity.Animator.runtimeAnimatorController = swordAnimatorController;
                break;
            case Buff.SwordArmor:
                _entity.Animator.runtimeAnimatorController = swordAndArmorAnimatorController;
                break;
            default:
                throw new System.Exception("Unknown buff variant: " + buffs);
        }
    }

    void Update()
    {
        // if (Application.isEditor) 
        // {
        //     // SetVariant(entity.ActiveBuff);
        //     // armorAnimatorController.animationClips[0].
        // }
    }
}