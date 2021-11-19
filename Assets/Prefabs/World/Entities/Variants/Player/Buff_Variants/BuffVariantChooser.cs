using UnityEngine;
using UnityEditor;

public class BuffVariantChooser : MonoBehaviour
{
    public Animator animator;

    public RuntimeAnimatorController armorAnimatorController;
    public RuntimeAnimatorController noItemAnimatorController;
    public RuntimeAnimatorController swordAnimatorController;
    public RuntimeAnimatorController swordAndArmorAnimatorController;

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
}