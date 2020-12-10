using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossStateManager : MonoBehaviour
{
    [SerializeField] IBossState currentState;
    public IBossState attackState = new BossAttackState();
    public IBossState idleInactiveState = new BossInactiveState();
    //public ICharState hurtState = new HurtState();
    //public ICharState overdriveState = new OverdriveState();
    IBossState newState;

    public Animator animator;

    //anim clip names, type in manually.
    public string BOSS_INACTIVE_IDLE_ANIM_NAME;
    public string BOSS_ATTACK_ANIM_NAME;
    public string BOSS_HURT_ANIM_NAME;
    public string BOSS_OVERDRIVE_ANIM_NAME;


    //Rig References
    [SerializeField] public GameObject idleInactiveRig;
    [SerializeField] public GameObject attackRig;
    [SerializeField] public GameObject overdriveRig;
    [SerializeField] public GameObject hurtRig;

    //skill prefab references
    [SerializeField] public List<GameObject> bossSkillPrefabs;

    //delegates
    public Action endBossTurn;


    private void Awake()
    {
        //subscribe to TurnSystems beginEnemyTurn and beginPlayerTurn delegates
        


        //the boss doesn't need his own delegates for now btw... the UI is paying attentino to the TurnSystem delegates..not boss
    }

    private void Start()
    {
        currentState = idleInactiveState;
        currentState.InitState(this);
    }
    private void Update()
    {
        newState = currentState.DoState(this);

        if (newState != currentState)
        {
            currentState = newState;
            currentState.InitState(this);
        }
           
    }
}
