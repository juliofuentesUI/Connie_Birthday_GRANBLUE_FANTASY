using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInactiveState : IBossState
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
        else if (boss.isOverdrive)
        {
            this.ExitState(boss);
            return boss.overdriveState;
        }
        else if (TurnSystem.isPlayerTurn)
        {
            return boss.idleInactiveState;
        }
        else
        {
            //this means its now THE ENEMY'S TURN. Go straight into attack...
            this.ExitState(boss);
            return boss.attackState;
        }
        //so basically... pay attention to the TuRNSYSTEM.

    }

    public void ExitState(BossStateManager boss)
    {
        //throw new System.NotImplementedException();
        boss.idleInactiveRig.SetActive(false);
    }

    public void InitState(BossStateManager boss)
    {
        //setActive the inactive rig.
        boss.idleInactiveRig.SetActive(true);
        boss.animator.Play(boss.BOSS_INACTIVE_IDLE_ANIM_NAME);
    }
}
