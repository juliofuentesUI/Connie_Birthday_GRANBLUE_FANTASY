using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : ICharState
{
    public ICharState DoState(CharStateManager thisCharacter)
    {
        if (thisCharacter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            this.ExitState(thisCharacter);
            return thisCharacter.idleInactiveState;
        }
        else
        {
            return thisCharacter.hurtState;
        }
    }

    public void ExitState(CharStateManager thisCharacter)
    {
        thisCharacter.isHurt = false;
        thisCharacter.hurtRig.SetActive(false);
    }

    public void InitState(CharStateManager thisCharacter)
    {
        thisCharacter.hurtRig.SetActive(true);
        SoundManager.Instance.Play(thisCharacter.hurtAudioList[Random.Range(0, thisCharacter.hurtAudioList.Count)]);
        thisCharacter.animator.Play(thisCharacter.CHAR_HURT);
    }
}
