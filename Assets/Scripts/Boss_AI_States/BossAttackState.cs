﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : IBossState
{
    public IBossState DoState(BossStateManager boss)
    {
        if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //if the animation is done playing. then go back to inactive state.
            this.ExitState(boss);
            return boss.idleInactiveState;
        }
        else
        {
            return boss.attackState;
        }
    }

    public void ExitState(BossStateManager boss)
    {
        boss.attackRig.SetActive(false);
        boss.endBossTurn?.Invoke();
        //call a delegate that says "DONE ATTACKING", turnSystem will listen.
    }

    public void InitState(BossStateManager boss)
    {
        //activate rig and play anim
        boss.attackRig.SetActive(true);
        boss.animator.Play(boss.BOSS_ATTACK_ANIM_NAME);
        //this only plays the RIGS animation. not the skill objects.
        //make sure here we pick a random skill..then we wait for animator to finish b4 going back to IdleInactive state. 
        //remember to ExitState properly, change bools , call the right delegates etc.
        //the TurnSystem.cs must listen to when the Boss is done attacking.
    }
}
