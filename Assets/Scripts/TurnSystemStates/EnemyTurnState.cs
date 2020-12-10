using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : ITurnState
{
    public ITurnState DoState(TurnSystem turnSystem)
    {
        //we exit the EnemyTurnState once ENEMY finishes attacking
        //now we must fill in the code and WIRE 
        if (TurnSystem.isPlayerTurn)
        {
            return turnSystem.playerTurnState;
        }
        else
        {
            return turnSystem.enemyTurnState;
        }
    }

    public void ExitState(TurnSystem turnSystem)
    {
    }

    public void InitState(TurnSystem turnSystem)
    {
        turnSystem.beginEnemyTurn?.Invoke();
    }
}
