using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFx : MonoBehaviour
{
    [SerializeField] AudioClip startGameSoundFx;
    public void PlayStartGameSoundFx()
    {
        SoundManager.Instance.Play(startGameSoundFx);
    }
}
