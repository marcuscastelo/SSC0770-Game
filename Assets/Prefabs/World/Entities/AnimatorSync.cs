using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSync : MonoBehaviour
{
    public Animator animator;
    public EntityController controller;

    // Update is called once per frame
    void Update()
    {
        if (animator.runtimeAnimatorController != null) {
            animator.SetFloat("speedX", controller.InputVector.x);
            animator.SetFloat("speedY", controller.InputVector.y);
        }
    }
}
