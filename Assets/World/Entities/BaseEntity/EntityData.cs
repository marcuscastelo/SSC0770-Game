using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData<Stats, RTI> : MonoBehaviour where Stats: EntityStats where RTI: EntityRTI
{
    // References
    public Animator animator;

    // Parameters (Stats)
    Stats stats;
    RTI runtimeInfo;

    void Start() {
        // TODO: assert nothing is null
    }
}