using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueLine : DialogueBaseClass
{
    [Header ("Dialogue Parameters")]
    [SerializeField] string textInput;
    [SerializeField] float textDelay;
    private Text textHolder;

    [Header("Sound")]
    [SerializeField] private AudioClip textSound;
    [SerializeField] private AudioClip soundFx;
    [SerializeField] private AudioClip music;

    [Header("Character Image")]
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image imageHolder;

    private void Awake()
    {
        textHolder = GetComponent<Text>();
        textHolder.text = "";
        imageHolder.sprite = characterSprite;
        //imageHolder.preserveAspect = true;
    }

    private void Start()
    {
        StartCoroutine(WriteText(textInput, textHolder, textDelay, textSound, music, soundFx));
    }

}
