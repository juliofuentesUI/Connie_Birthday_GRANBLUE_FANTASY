using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour 
{
    [SerializeField] SceneLoader sceneLoader;
    private void Awake()
    {
        StartCoroutine(dialogueSequence());
    }
    private IEnumerator dialogueSequence()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Deactivate();
            transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
        }
        //this means all LINES have ended
        gameObject.SetActive(false);
        sceneLoader.LoadNextScene();
    }

    private void Deactivate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
