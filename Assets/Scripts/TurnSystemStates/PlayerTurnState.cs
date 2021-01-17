using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : ITurnState
{
    //Currently, TurnSystem is only reacting to UI_ANIMCONTROLLER 
    public ITurnState DoState(TurnSystem turnSystem)
    {
        //here...
        if (TurnSystem.isPlayerTurn == false)
        {
            return turnSystem.enemyTurnState;
        }
        else
        {
            return turnSystem.playerTurnState;
        }
    }

    public void ExitState(TurnSystem turnSystem)
    {
        //not much to do here i think.
    }

    public void InitState(TurnSystem turnSystem)
    {
        //invoke the delegate to trigger the proper setup...AKA putting the enemy boss into inactive state..well he'll be there by default but yeah.
        turnSystem.beginPlayerTurn?.Invoke();
    }
}
