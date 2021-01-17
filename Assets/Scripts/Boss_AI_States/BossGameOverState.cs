using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameOverState : IBossState
{
    public IBossState DoState(BossStateManager boss)
    {
        return boss.gameOverState;
    }

    public void ExitState(BossStateManager boss)
    {
        //do nothing
    }

    public void InitState(BossStateManager boss)
    {
        //do nothing
    }
}
