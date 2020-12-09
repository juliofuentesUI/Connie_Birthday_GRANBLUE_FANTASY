using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleActiveState : ICharState
{
    public ICharState DoState(CharStateManager thisCharacter)
    {
        Debug.Log("CurrentState is IdleActiveState");
        //we must do it this way because ATTACKING takes precedence.
        //this is very bad code because you don't know who the hell toggles isAttacking to true to begin with.
        //we know its UI_ANIMControlller but still
        if (thisCharacter.isAttacking)
        {
            this.ExitState(thisCharacter);
            return thisCharacter.attackState;
        }
        else if (thisCharacter.isSelected == false)
        {
            this.ExitState(thisCharacter);
            return thisCharacter.idleInactiveState;
        }
        else if (thisCharacter.isBuffing)
        {
            this.ExitState(thisCharacter);
            return thisCharacter.buffState;
        }
        else
        {
            //return our current state.
            return thisCharacter.idleActiveState;
        }

    }

    public void ExitState(CharStateManager thisCharacter)
    {
        thisCharacter.idleActiveRig.SetActive(false);
    }

    public void InitState(CharStateManager thisCharacter)
    {
        thisCharacter.idleActiveRig.SetActive(true);
        thisCharacter.animator.Play(thisCharacter.CHAR_ACTIVE_IDLE);
    }
}
