using UnityEngine;
using UnityEditor;

using Hypnos.Entities;

[ExecuteInEditMode]
public class BuffVariantChooser : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Entity entity;

    [SerializeField] private RuntimeAnimatorController armorAnimatorController;
    [SerializeField] private RuntimeAnimatorController noItemAnimatorController;
    [SerializeField] private RuntimeAnimatorController swordAnimatorController;
    [SerializeField] private RuntimeAnimatorController swordAndArmorAnimatorController;

    public void Start()
    {
        entity.Buff.OnBuffChangedEvent += SetVariant;
    }

    public void SetVariant(Buff buffs)
    {
        switch (buffs)
        {
            case Buff.Armor:
                animator.runtimeAnimatorController = armorAnimatorController;
                break;
            case Buff.NoItem:
                animator.runtimeAnimatorController = noItemAnimatorController;
                break;
            case Buff.Sword:
                animator.runtimeAnimatorController = swordAnimatorController;
                break;
            case Buff.SwordArmor:
                animator.runtimeAnimatorController = swordAndArmorAnimatorController;
                break;
            default:
                throw new System.Exception("Unknown buff variant: " + buffs);
        }
    }

    void Update()
    {
        if (Application.isEditor) 
        {
            SetVariant(entity.Buff.ActiveBuff);
        }
    }
}