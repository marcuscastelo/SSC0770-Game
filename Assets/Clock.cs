using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // public bool paused = false;
    // public float time = 0;
    // public float timeScale = 1;
    public bool late = false; // TODO: remove this and determine if late in the update loop
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetBool("late", late);
    }
}
