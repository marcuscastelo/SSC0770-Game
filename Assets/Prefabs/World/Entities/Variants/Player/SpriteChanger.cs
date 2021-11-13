using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public RuntimeAnimatorController noItemAnimatorController;
    // public RuntimeAnimatorController swordAnimatorController;
    public RuntimeAnimatorController armorAnimatorController;
    // public RuntimeAnimatorController swordArmorAnimatorController;

    public void OnBuffChanged(Buff enabledBuffs)
    {
        int currentFrame = (int) animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

        // if (enabledBuffs == Buff and armor)
        if ((enabledBuffs & Buff.Armor) == Buff.Armor)
        {
            animator.runtimeAnimatorController = armorAnimatorController;
        }
        else if ((enabledBuffs & Buff.Sword) == Buff.Sword)
        {
            // animator.runtimeAnimatorController = armorAnimatorController;
        }
        else
        {
            animator.runtimeAnimatorController = noItemAnimatorController;
        }

        // Sets the current frame to the same frame as before
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, currentFrame);
    }

    public void Start() {
        AssertNotNull();
    }

    void AssertNotNull() {
        //Assert not null, printing current scene and game object name
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string gameObjectName = gameObject.name;

        if (spriteRenderer == null) {
            Debug.LogError("SpriteRenderer is null in " + sceneName + " " + gameObjectName);
        }
        if (animator == null) {
            Debug.LogError("Animator is null in " + sceneName + " " + gameObjectName);
        }
        if (noItemAnimatorController == null) {
            Debug.LogError("noItemAnimatorController is null in " + sceneName + " " + gameObjectName);
        }
        // if (swordAnimatorController == null) {
        //     Debug.LogError("swordAnimatorController is null in " + sceneName + " " + gameObjectName);
        // }
        if (armorAnimatorController == null) {
            Debug.LogError("armorAnimatorController is null in " + sceneName + " " + gameObjectName);
        }
        // if (swordArmorAnimatorController == null) {
        //     Debug.LogError("swordArmorAnimatorController is null in " + sceneName + " " + gameObjectName);
        // }
    }


}
