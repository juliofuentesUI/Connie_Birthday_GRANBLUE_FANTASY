using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffState : ICharState
{
    public ICharState DoState(CharStateManager thisCharacter)
    {
        //we're only in buff state up until the animation finishes playing.
        //do what we did in attack state then.
        if (thisCharacter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //animation has finished.
            //BuffState can only go back to activeState just fyi
            this.ExitState(thisCharacter);
            return thisCharacter.idleActiveState; 
        } 
        else
        {
            return thisCharacter.buffState;
        }
    }

    public void ExitState(CharStateManager thisCharacter)
    {
        thisCharacter.buffRig.SetActive(false);
        thisCharacter.isBuffing = false;
    }

    public void InitState(CharStateManager thisCharacter)
    {
        thisCharacter.buffRig.SetActive(true);
        thisCharacter.animator.Play(thisCharacter.CHAR_BUFF);
        //because this is a buff..we need to make sure this plays in 3 diff positions...so...just call it 3 times!
        //each with the proper ally positions
        foreach(var character in thisCharacter.gameManager.currentCharacters)
        {
            //debug this later to ffind out the type.
            thisCharacter.currentSkillObject.PlaySkillAnimation(character.transform.position);
        }
    }
}
