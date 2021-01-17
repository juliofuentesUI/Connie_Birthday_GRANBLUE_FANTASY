using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : IBossState
{
    public IBossState DoState(BossStateManager boss)
    {
        if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            this.ExitState(boss);
            return boss.gameOverState;
        }
        else
        {
            return boss.deathState;
        }
    }

    public void ExitState(BossStateManager boss)
    {
        boss.deathRig.SetActive(false);
        boss.questClearedEvent.EnableQuestClearedMenu();
        //invoke quest cleared NOW
        //actually, invoke bossIsDead now
        //play victory music
        //the VICTORY UI screen will play the music!
    }

    public void InitState(BossStateManager boss)
    {
        //turn on rig. play animation on loop
        boss.deathRig.SetActive(true);
        boss.animator.Play(boss.BOSS_DEATH_ANIM_NAME);
        //play the audio scream from here...but play explosions in the rig anim keyframes
        //AudioClip deathScream
        SoundManager.Instance.PlayVoice(boss.deathScreamSE);

        //invoke a delegate to block the screen UI from input so we can watch death animation.
    }
}
