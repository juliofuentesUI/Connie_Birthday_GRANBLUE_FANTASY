using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ICharState
{
    public ICharState DoState(CharStateManager thisCharacter)
    {
        Debug.Log("CurrentState is Attack");
        //PERFORM YOUR ATTACK
        //all we do here is make sure the animation is finished playing
        //Debug.Log($"The current value of AnimatorStateInfo(0).noramlizedTime is : {thisCharacter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime }");
        if (thisCharacter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //we have finished playing attack animation, now return idleActive
            this.ExitState(thisCharacter);
            return thisCharacter.idleActiveState;
        } 
        else
        {
            return thisCharacter.attackState;
        }
    }

    public void ExitState(CharStateManager thisCharacter)
    {
        thisCharacter.attackRig.SetActive(false);
        thisCharacter.isAttacking = false;
    }

    public void InitState(CharStateManager thisCharacter)
    {
        //activate rig, play animation.
        thisCharacter.attackRig.SetActive(true);
        thisCharacter.animator.Play(CharStateManager.CHAR_ATTACK);
        thisCharacter.currentSkillObject.PlaySkillAnimation();
    }
}
