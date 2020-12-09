using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStateManager : MonoBehaviour
{
    [SerializeField] ICharState currentState;
    ICharState newState;
    public ICharState idleInactiveState = new IdleInactiveState();
    public ICharState idleActiveState = new IdleActiveState();
    public ICharState attackState = new AttackState();
    public ICharState buffState = new BuffState();
    public Animator animator;
    public bool isSelected;
    public bool isAttacking;
    public bool isBuffing;
    public SkillObject currentSkillObject;
    //[SerializeField] List<GameObject> skillPrefabs;
    public Dictionary<string, SkillObject> skillDictionary = new Dictionary<string, SkillObject>();
    [SerializeField] int _skillCostRemaining;
    public int skillCostRemaining { get { return _skillCostRemaining; } private set { _skillCostRemaining = value; } }


    //type in the name of the anim clips. type in manually in inspector
    public string CHAR_INACTIVE_IDLE;
    public string CHAR_ACTIVE_IDLE;
    public string CHAR_ATTACK;
    public string CHAR_HURT;
    public string CHAR_BUFF;

    //we need hard references to the RIGS to enable/disable them.
    [SerializeField] public GameObject idleInactiveRig;
    [SerializeField] public GameObject attackRig;
    [SerializeField] public GameObject idleActiveRig;
    [SerializeField] public GameObject hurtRig;
    [SerializeField] public GameObject buffRig;

    //we need to add our gameInstance to the manager.
    public GameManager gameManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddCharacterToList(this);
        skillCostRemaining = 2;
    }
    void Start()
    {
        currentState = idleInactiveState;
        currentState.InitState(this);
    }

    public void DecrementSkillCost()
    {
        //should get called anytime we go to BuffState or AttackState.
        skillCostRemaining--;
    }

    public void ResetSkillCost()
    {
        //this should only happen when going from EnemyTurn to PlayerTurn transition.
        skillCostRemaining = 2;
    }

    public void InitAttack(string attackName)
    {
        //could be a BUFF tho...
        Debug.Log("Init attack or buff");
        SkillObject attackSkillObject;
        skillDictionary.TryGetValue(attackName, out attackSkillObject);
        if (attackSkillObject != null && attackSkillObject.skillType == "ATTACK")
        {
            //then initialize isAttacking to true. our state will react to it.
            //but we need to pass our AttackState the skillObject.
            isAttacking = true;
            currentSkillObject = attackSkillObject;
        } 
        else if (attackSkillObject != null && attackSkillObject.skillType == "BUFF")
        {
            //wee need to trigger the BUFF state...how tho? well.. it can only transition to BUFF state from IDLEACTIVE
            //so idle active needs to be told to go to BUFF state. 
            //just follow the same pattern for now...
            isBuffing = true;
            currentSkillObject = attackSkillObject;
        }
        else
        {
            return;
        }
    }





    // Update is called once per frame
    void Update()
    {
        newState = currentState.DoState(this);
        if (currentState != newState)
        {
            //so if we DO change states. we will call Init, 
            //then DoState will be called next frame. So dont have Init call DoState();
            currentState = newState;
            currentState.InitState(this);
        }
    }
}
