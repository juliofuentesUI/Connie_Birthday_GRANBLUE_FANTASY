using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtState : IBossState
{
    public IBossState DoState(BossStateManager boss)
    {
        if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //anim has ended
            this.ExitState(boss);
            if (boss.isDead)
            {
                return boss.deathState;
            }

            if (boss.isOverdrive)
            {
                //this overDriveStart invocation..should only run ... once wtf...
                //boss.overdriveStart?.Invoke();
                return boss.overdriveState;
            }
            else
            {
                return boss.idleInactiveState;
            }
        }
        else
        {
            return boss.hurtState;
        }
    }

    public void ExitState(BossStateManager boss)
    {
        boss.hurtRig.SetActive(false);
        boss.isHurt = false;
    }

    public void InitState(BossStateManager boss)
    {
        //activate rig
        boss.hurtRig.SetActive(true);
        boss.animator.Play(boss.BOSS_HURT_ANIM_NAME);
        AudioClip randomHurtClip = boss.hurtAudioClips[Random.Range(0, boss.hurtAudioClips.Count)];
        SoundManager.Instance.PlayVoice(randomHurtClip);
    }
}
