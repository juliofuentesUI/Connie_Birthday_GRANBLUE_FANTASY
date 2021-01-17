using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip mainMenuMusic;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(mainMenuMusic);
    }
}
