using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossStateManager : MonoBehaviour
{
    [SerializeField] IBossState currentState;
    public IBossState attackState = new BossAttackState();
    public IBossState idleInactiveState = new BossInactiveState();
    public IBossState hurtState = new BossHurtState();
    public IBossState overdriveState = new BossOverdriveState();
    public IBossState deathState = new BossDeathState();
    public IBossState gameOverState = new BossGameOverState();
    IBossState newState;

    public Animator animator;

    //anim clip names, type in manually.
    public string BOSS_INACTIVE_IDLE_ANIM_NAME;
    public string BOSS_ATTACK_ANIM_NAME;
    public string BOSS_HURT_ANIM_NAME;
    public string BOSS_OVERDRIVE_ANIM_NAME;
    public string BOSS_DEATH_ANIM_NAME;


    //Rig References
    [SerializeField] public GameObject idleInactiveRig;
    [SerializeField] public GameObject attackRig;
    [SerializeField] public GameObject overdriveRig;
    [SerializeField] public GameObject hurtRig;
    [SerializeField] public GameObject deathRig;

    //skill prefab references
    [SerializeField] public List<GameObject> bossSkillPrefabs;

    //delegates
    public Action endBossTurn;
    public Action overdriveStart;

    //bools
    public bool isHurt;
    public bool isOverdrive;
    public bool isDead;
    public static bool isOverdriveStatic;

    //boss is hurt audio.
    public List<AudioClip> hurtAudioClips;
    public List<AudioClip> attackAudioClips;

    //boss music ost
    public AudioClip overdriveMusic;
    public AudioClip overdriveSE;
    public AudioClip deathScreamSE;


    public QuestCleared questClearedEvent;

    public void InitOverdrive()
    {
        isOverdriveStatic = true;
        isOverdrive = true;
        SoundManager.Instance.PlayMusic(overdriveMusic);
        //BossOvedriveState will now invoke overdriveStart event
    }


    private void Awake()
    {
        isOverdriveStatic = false;
        //subscribe to the player HAS attacked delegate in CharState Manager


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
