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
    public Animator animator;
    public bool isSelected;
    public bool isAttacking;
    public SkillObject currentSkillObject;
    //[SerializeField] List<GameObject> skillPrefabs;
    public Dictionary<string, SkillObject> skillDictionary = new Dictionary<string, SkillObject>();

    //Const animation states
    public const string CHAR_INACTIVE_IDLE = "character idle";
    public const string CHAR_ACTIVE_IDLE = "idleActiveAnim";
    public const string CHAR_ATTACK = "character attack";

    //we need hard references to the RIGS to enable/disable them.
    [SerializeField] public GameObject idleInactiveRig;
    [SerializeField] public GameObject attackRig;
    [SerializeField] public GameObject idleActiveRig;


    public void InitAttack(string attackName)
    {
        //access the attack animation in the dictionary.
        Debug.Log("Init attack");
        SkillObject attackSkillObject;
        skillDictionary.TryGetValue(attackName, out attackSkillObject);
        if (attackSkillObject != null)
        {
            //then initialize isAttacking to true. our state will react to it.
            //but we need to pass our AttackState the skillObject.
            isAttacking = true;
            currentSkillObject = attackSkillObject;
        } 
        else
        {
            return;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentState = idleInactiveState;
        currentState.InitState(this);
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
