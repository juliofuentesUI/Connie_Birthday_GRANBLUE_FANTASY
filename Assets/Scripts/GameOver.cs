using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //in awake, bind to isDead delegate in UnitStats.
    //manage animation and load scene logic here.
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] AudioClip gameOverMusic;
    int deathCount = 0;
    private void OnEnable()
    {
        
    }
    private void Awake()
    {
       //bind to the is dying event 
    }

    public void SubscribeSelfToDelegate(Action playerDelegate)
    {
        playerDelegate += IncrementDeathCount;        
    }
    //THE ABOVE FUNCTION WE DON'T EVEN USE IT. 

    public void IncrementDeathCount()
    {
        deathCount++;
        Debug.Log($"Deathcount is at : {deathCount}");
        if (deathCount == 3)
        {
            //enable gameOver screen, player has loss , boss wins.
            EnableGameOverMenu();
        }
    }

    private void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        SoundManager.Instance.PlayMusic(gameOverMusic, false);
    }

}
