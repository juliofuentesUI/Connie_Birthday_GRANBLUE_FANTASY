using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterAnimator : MonoBehaviour
{
    [SerializeField] BossStateManager boss;
    [SerializeField] TurnSystem turnSystem;
    [SerializeField] Sprite overdrivePortrait;
    Animator animator;
    //Setup your delegates
    private bool skipFirstTurnAnim = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        turnSystem.beginPlayerTurn += PlayerTurnAnim;
        turnSystem.beginEnemyTurn += EnemyTurnAnim;
        
    }

    public void PlayerTurnAnim()
    { 
        //this is so we don't see the PLAYER TURN anim behind the 
        //boss battle animation

        if (skipFirstTurnAnim)
        {
            skipFirstTurnAnim = false;
            return;
        }

        this.animator.Play("PlayerTurn_Anim");
    }

    public void EnemyTurnAnim()
    {
        this.animator.Play("EnemyTurn_Anim");
    }
}
