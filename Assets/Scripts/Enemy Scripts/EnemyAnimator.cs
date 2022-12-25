using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Walk(bool walk)
    {
        animator.SetBool(AnimationTag.WALK_PARAMETER, walk);
    }
    public void Run(bool run)
    {
        animator.SetBool(AnimationTag.RUN_PARAMETER, run);
    }
    public void Attack()
    {
        animator.SetTrigger(AnimationTag.ATTACK_TRIGGER);
    }
    public void Dead()
    {
        animator.SetTrigger(AnimationTag.DEAD_TRIGGER);
    }
}
