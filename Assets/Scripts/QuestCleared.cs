using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCleared : MonoBehaviour
{
    [SerializeField] GameObject questClearedMenu;
    [SerializeField] AudioClip questClearedSE;
    [SerializeField] AudioClip questClearedMusic;


    public void EnableQuestClearedMenu()
    {
        questClearedMenu.SetActive(true);
        SoundManager.Instance.Play(questClearedSE);
        SoundManager.Instance.PlayMusic(questClearedMusic);
    }
}
