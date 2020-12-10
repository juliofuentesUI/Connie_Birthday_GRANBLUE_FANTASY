using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] ITurnState currentState;
    public ITurnState playerTurnState = new PlayerTurnState();
    public ITurnState enemyTurnState = new EnemyTurnState();
    private ITurnState newState;
    public static bool isPlayerTurn { get; private set; }


    //Action events for other ppl to animate to. like a master animator.cs class
    public Action beginPlayerTurn;
    public Action beginEnemyTurn;

    void Start()
    {
        currentState = playerTurnState;
        isPlayerTurn = true;
        currentState.InitState(this);
        FindObjectOfType<UI_AnimController>().skipTurnDelegate += () => isPlayerTurn = false;
        FindObjectOfType<BossStateManager>().endBossTurn += () => isPlayerTurn = true;
        //go create the enemyAISTATE MACHINE.
        //FindObjectOfType<EnemyAIStateMachine>().attackOverDelegate += () => isPlayerTurn = true;
    }

    void Update()
    {
        newState = currentState.DoState(this);
        if (newState != currentState)
        {
            currentState = newState;
            currentState.InitState(this);
        }
        
    }
}
