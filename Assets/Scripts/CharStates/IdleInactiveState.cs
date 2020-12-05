using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleInactiveState : ICharState
{
    public ICharState DoState(CharStateManager thisCharacter)
    {
        Debug.Log("CurrentState is idleINACTIVE state");
        //return some iCharState or null FOR NOW
        // this can only transition to IdleActive or HURT
        //condition should be... if a player is selected.
        if (thisCharacter.isSelected)
        {
            //if this character is selected, then switch to IdleActive state, and in that state play the idleActiveAnim
            Debug.Log("thisCharacter.isSelected is true");
            this.ExitState(thisCharacter);
            return thisCharacter.idleActiveState;
        }
        else
        {
            //play character idle inactive anim, return same state.
            //ACTUALLY, WE DO THAT IN THE INIT STATE!
            return thisCharacter.idleInactiveState;
        }
        //this state can only go to HURT. or active Idle
    }

    public void ExitState(CharStateManager thisCharacter)
    {
        Debug.Log("Exiting State in idleInactiveState");
        //don't worry abourt undoing in this state.
        //let the next states Init method do the changing of anims
        thisCharacter.idleInactiveRig.SetActive(false);
    }

    public void InitState(CharStateManager thisCharacter)
    {
        Debug.Log("InitState IdleInactiveState");
        thisCharacter.idleInactiveRig.SetActive(true);
        thisCharacter.animator.Play(CharStateManager.CHAR_INACTIVE_IDLE);
    }
}
