using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState 
{
    IBossState DoState(BossStateManager boss);
    void ExitState(BossStateManager boss);
    void InitState(BossStateManager boss);
}
