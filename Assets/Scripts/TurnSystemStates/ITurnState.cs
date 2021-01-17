using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnState  
{
    void InitState(TurnSystem turnSystem);
    void ExitState(TurnSystem turnSystem);

    ITurnState DoState(TurnSystem turnSystem);
}
