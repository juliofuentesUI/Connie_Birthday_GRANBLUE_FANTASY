using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [SerializeField] int maxHealth = 1500;
    [SerializeField] int currentHealth;
    [SerializeField] private bool defenseUp = false;
    [SerializeField] private bool attackUp = false;

    [SerializeField] GameObject healthBarPrefab;
    public HealthBar healthBar;

    public bool isBoss;
    public bool isDead;

    public Action playerIsDead;
    public Action<bool> defenseIsUp;
    public Action<bool> attackIsUp;
    public Action<bool> healthIsUp;

    GameOver gameOver;
    QuestCleared questClearedScript;
    GameManager gameManager;
    BossStateManager boss;
    [SerializeField] GameObject inputBlockerUi;

    private void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        GameObject healthBarInstance = Instantiate(healthBarPrefab, canvas.transform);
        healthBar = healthBarInstance.GetComponent<HealthBar>();
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
        gameOver = FindObjectOfType<GameOver>();
        questClearedScript = FindObjectOfType<QuestCleared>();
        boss = FindObjectOfType<BossStateManager>();
        TurnSystem turnSystem = FindObjectOfType<TurnSystem>();
        turnSystem.beginPlayerTurn += () => DefenseUp(false);
        turnSystem.beginEnemyTurn += () => AttackUp(false);
        turnSystem.beginPlayerTurn += () => ResetHealthStatusIcon();

        gameManager = FindObjectOfType<GameManager>();
        //wire up tp TurnSystem
    }

    public void AddHealth(int healthUp)
    {
        currentHealth += healthUp;
    }

    public void TakeDamage(int damage)
    {
        if (isBoss && BossStateManager.isOverdriveStatic)
        {
            damage = damage / 2;
        }

        if (isBoss && gameManager.currentCharacters[0].GetComponent<UnitStats>().attackUp)
            damage = damage + (damage / 2);

        if (defenseUp)
            damage = damage / 2;


        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthBar.SetHealth(0);
            if (isBoss)
            {
                BossDefeated();
            }
            else
            {
                IsDead();
            }
            //add an if else to see if life at 50%. modifier kicks in trigger OVERDRIVE on boss.
        }
        else
        {
            healthBar.SetHealth(currentHealth);
            if (isBoss)
                boss.isHurt = true;
        }
    }

    private void BossDefeated()
    {
        Debug.Log("BOSS HAS LOSS");
        FindObjectOfType<BossStateManager>().isDead = true;
        //the deathState will trigger the questCleared only after the death animation finished.
        inputBlockerUi.SetActive(true);
        //in the meantime. turn on the input blocker.
        
    }

    private void IsDead()
    {
        if (isBoss) return;
        isDead = true;
        gameOver.IncrementDeathCount();
        //find this UnitStats GameObject which is the character prefab.
        //gameObject

        //trigger MasterAnimator to show Quest failed.
        //black screen enabled with 2 buttons to restart or main menu.
        //is dead must differentiate between BOSS and PLAYER
        //play GAME OVER MUSIC! ONLY FOR player not boss.
        //UI anim controller will react with gameOver method attached to this 
        //in that gameOver it'll call ShowPartySelectUI and then make the UI non-interactable.
        //then we will watch the boss explode and die and then boom quest cleared.

        //wait for all 3 players to be dead...
        //if a player dies...we must delete ONE of the command panels.
        //TRIGGER BLOCKRAYCAST AS SOON AS THIS PLAYER DIES!! 
    }

    public void DefenseUp(bool toggle = true)
    {
        defenseUp = toggle;
        defenseIsUp?.Invoke(toggle);
    }
    public void AttackUp(bool toggle = true)
    {
        //attackUp is actually useless buff in this current state.
        attackUp = toggle;
        attackIsUp?.Invoke(toggle);
    }

    public void HealthUp(int healthAmount, bool toggle = true)
    {
        Debug.Log("HEALTH UP SHOULD ONLY RUN 3 TIMES");
        //WARNING: THIS IS LITEARLLY RUNNIG 9 TIMES. BECAUSE I RUN THIS SAME ANIMATION IN 3 LOCATIONS CAUSE ALLIEBUFFS
        currentHealth += healthAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
        healthIsUp?.Invoke(toggle);
    }

    public void ResetHealthStatusIcon()
    {
        healthIsUp?.Invoke(false);
    }
}
