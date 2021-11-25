using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EntityCombat: MonoBehaviour, IAttackable, IAttacker
{
    [SerializeField] private CombatStats combatStats;
    [SerializeField] private Collider2D attackerArea;
    [SerializeField] private bool attackAreaAlwaysEnabled = false;

    [SerializeField] private int startFrame = 0;
    [SerializeField] private int endFrame = 1;
    [SerializeField] private int totalFrames = 1;

    public CombatStats Stats => combatStats;

    void Awake()
    {
        if (attackerArea != null)
            attackerArea.enabled = attackAreaAlwaysEnabled;

        Debug.Assert(startFrame < endFrame, "EntityCombat.Awake() - startFrame must be less than endFrame");
        Debug.Assert(endFrame <= totalFrames, "EntityCombat.Awake() - endFrame must be less than or equal to totalFrames");
    }

    private IEnumerator BlinkAttackAreaCoroutine(float duration)
    {
        float frameDuration = duration / totalFrames;
        if (attackerArea != null)
        {
            yield return new WaitForSeconds(startFrame * frameDuration);
            attackerArea.enabled = true;
            yield return new WaitForSeconds((endFrame - startFrame) * frameDuration);
            attackerArea.enabled = false;
        }
    }

    public void Attack()
    {
        StartCoroutine(BlinkAttackAreaCoroutine(combatStats.attackDuration));
    }

    public void OnAttacked(IAttacker attacker)
    {
        Debug.Log("EntityCombat.OnAttacked()");
    }

    public void OnValidate()
    {
        if (attackerArea != null)
            attackerArea.enabled = attackAreaAlwaysEnabled;
        

        if (startFrame >= endFrame) {
            Debug.LogWarning("EntityCombat.OnValidate() - startFrame must be less than endFrame");
            endFrame = startFrame + 1;
        }

        if (endFrame >= totalFrames) {
            Debug.LogWarning("EntityCombat.OnValidate() - endFrame must be less than or equal to totalFrames");
        }

        totalFrames = Mathf.Max(totalFrames, endFrame+1);
    }
}