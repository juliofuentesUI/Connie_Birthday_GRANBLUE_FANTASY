using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }
    public AudioSource MusicSource;
    public AudioSource EffectsSource;
    public AudioSource VoiceOvers;
    private void Awake()
    {
        MusicSource.loop = true;
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip audioClip, float volume = 1)
    {
        //EffectsSource.clip = audioClip;
        EffectsSource.volume = volume;
        EffectsSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip musicClip, bool letLoop = true)
    {
        MusicSource.clip = musicClip;
        MusicSource.loop = letLoop;
        MusicSource.Play();
    }
    public void PlayVoice(AudioClip voiceClip, float volume = 1)
    {
        VoiceOvers.volume = volume;
        VoiceOvers.PlayOneShot(voiceClip);
    }

    public void StartFadeOutCoroutine()
    {
        StartCoroutine(FadeoutMusic());
    }

    public IEnumerator FadeoutMusic(float targetDuration = 4.5f)
    {
        //float audioVolume = MusicSource.volume;
        float start = MusicSource.volume;
        float currentTime = 0f;
        //WaitForSeconds fadeDelay = new WaitForSeconds(fadeOutDelay);
        while (currentTime < targetDuration)
        {
            currentTime += Time.deltaTime;
            MusicSource.volume = Mathf.Lerp(start, 0, currentTime / targetDuration);
            yield return null;
        }
        //reset volume back to 1 if needed.
        //this reset volume back to 1 breaks this functions responsibility to only fadeout. but idc
        MusicSource.volume = 1f;
    }
}
