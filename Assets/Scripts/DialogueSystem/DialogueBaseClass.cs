using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBaseClass : MonoBehaviour
{
    public bool finished { get; private set; }
    protected IEnumerator WriteText(string sentence, Text textHolder, float delay, AudioClip sound, AudioClip music = null, AudioClip soundFx = null)
    {
        //do this to give textHolder some indentation
        textHolder.text = "     ";
        if (music != null)
            SoundManager.Instance.PlayMusic(music);
        if (soundFx != null)
            SoundManager.Instance.PlayVoice(soundFx);
        //play music soundtrack.
        WaitForSeconds delayBetweenLetters = new WaitForSeconds(delay);
        foreach(char letter in sentence.ToCharArray())
        {
            textHolder.text += letter;
            SoundManager.Instance.Play(sound);
            yield return delayBetweenLetters;
        }
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        finished = true;
    }
}
