using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioDuringAnim : MonoBehaviour
{
    [SerializeField] AudioClip audioClipSe;
    [SerializeField] AudioClip audioClipSe2;
    [SerializeField] AudioClip audioClipSe3;
    [SerializeField] AudioClip audioClipSe4;

    [SerializeField] float volume = 1f;
    public void PlayAudio()
    {
        if (audioClipSe)
            SoundManager.Instance.Play(audioClipSe, volume);
    }

    public void PlayAudioClip2()
    {
        if (audioClipSe2)
            SoundManager.Instance.Play(audioClipSe2, volume);
    }
    public void PlayAudioClip3()
    {
        if (audioClipSe3)
            SoundManager.Instance.Play(audioClipSe3, volume);
    }
    public void PlayAudioClip4()
    {
        if (audioClipSe4)
            SoundManager.Instance.Play(audioClipSe4, volume);
    }

}
