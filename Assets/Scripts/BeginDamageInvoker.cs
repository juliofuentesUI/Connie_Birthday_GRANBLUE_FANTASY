using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginDamageInvoker : MonoBehaviour
{
    BossStateManager boss;
    GameManager gameManager;
    [SerializeField] int damage;
    [SerializeField] int healthUpAmount;

    void Start()
    {
        boss = FindObjectOfType<BossStateManager>();
        gameManager = FindObjectOfType<GameManager>();
    }


    void BeginHurtingBoss()
    {
        boss.GetComponent<UnitStats>().TakeDamage(damage);
    }

    void BeginHurtingPlayers()
    {
        foreach(var player in gameManager.currentCharacters)
        {
            //Toggle bool 1st to trigger that units hurt animation
            player.isHurt = true;
            //now deal the damage in UnitStats of that Unit
            //UnitStats will update their respective HP lifebar
            if (BossStateManager.isOverdriveStatic)
            {
                player.GetComponent<UnitStats>().TakeDamage(damage * 2);
            }
            else
            {
                player.GetComponent<UnitStats>().TakeDamage(damage);
            }
            //make damage a LITTLE random for each player.
        }
    }

    void BuffPlayerDefense()
    {
        foreach(var player in gameManager.currentCharacters)
        {
            player.GetComponent<UnitStats>().DefenseUp();
        }
    }

    void BuffPlayerHealth()
    {
        foreach(var player in gameManager.currentCharacters)
        {
            player.GetComponent<UnitStats>().HealthUp(healthUpAmount);
        }
    }

    void BuffPlayerAttack()
    {
        foreach(var player in gameManager.currentCharacters)
        {
            player.GetComponent<UnitStats>().AttackUp();
        }

    }
}
