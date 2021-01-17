using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableHeroConfig")]
public class ScriptableHero : ScriptableObject
{
#pragma warning disable 0649
    [SerializeField] Sprite charSprite;
    [SerializeField] string charName;
    [SerializeField] ScriptableSkills[] charSkills;
    [SerializeField] GameObject characterPrefab;
    //dont forget voice files.
    [SerializeField] AudioClip[] audioFx;
    //char animations.
    //might make them dictionary to know they're names
    [SerializeField] AnimationClip[] animations;
    [SerializeField] GameObject spawnPosition;
    //attack data etc.
    #pragma warning restore 0649

    public Sprite GetCharSprite()
    {
        return charSprite;
    }
    public string GetCharName()
    {
        return charName;
    }

    public ScriptableSkills[] GetCharSkills()
    {
        return charSkills;
    }

    public GameObject GetHeroPrefab()
    {
        return characterPrefab;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.GetComponent<Transform>().position;
    }
}
