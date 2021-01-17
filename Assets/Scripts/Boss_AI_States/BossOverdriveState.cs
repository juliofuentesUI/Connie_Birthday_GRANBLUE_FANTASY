using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOverdriveState : IBossState
{
    public IBossState DoState(BossStateManager boss)
    {
        if (boss.isDead)
        {
            this.ExitState(boss);
            return boss.deathState;
        }

        if (TurnSystem.isPlayerTurn && boss.isHurt)
        {
            this.ExitState(boss);
            return boss.hurtState;
        }
        else if (TurnSystem.isPlayerTurn)
        {
            return boss.overdriveState;
        }
        else
        {
            //this means its now THE ENEMY'S TURN. Go straight into attack...
            this.ExitState(boss);
            return boss.attackState;
        }

    }

    public void ExitState(BossStateManager boss)
    {
        boss.overdriveRig.SetActive(false);
    }

    public void InitState(BossStateManager boss)
    {
        boss.overdriveRig.SetActive(true);
        boss.animator.Play(boss.BOSS_OVERDRIVE_ANIM_NAME);
        SoundManager.Instance.Play(boss.overdriveSE);
        //ghetto fix. moving boss.overDriveStart?.Invoke() to the exitState of hurtState
        //cause race condition
        boss.overdriveStart?.Invoke();
    }
}
