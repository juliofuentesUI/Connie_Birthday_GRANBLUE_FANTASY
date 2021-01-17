using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyTextAnimScript : MonoBehaviour
{
    [SerializeField] AudioClip readyTextSE;
    public void PlayReadyTextAudio()
    {
        SoundManager.Instance.Play(readyTextSE, 0.5f);
    }
}
