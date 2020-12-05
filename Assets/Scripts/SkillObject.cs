using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject
{
    public string skillName { get; private set;}
    public string skillAnimationClipName { get; private set;}
    public float skillDamage { get; private set;} 
    public Sprite skillSprite { get; private set;} 
    public string skillDescription { get; private set;} 
    public string skillType { get; private set;} 
    public GameObject skillAnimPrefab { get; private set;} 

    public void PlaySkillAnimation(Vector3? targetPosition = null)
    {
        //DONT FORGET TARGETPOSITION
        GameObject skillPrefabHandle = GameObject.Instantiate(skillAnimPrefab);
        skillPrefabHandle.SetActive(true);
        Animator animator = skillPrefabHandle.GetComponent<Animator>();
        animator.Play(skillAnimationClipName);
    }
    public SkillObject(string skillName, float skillDamage, Sprite skillSprite,
                        string skillDescription, string skillType, GameObject skillAnimPrefab, string skillAnimationClipName)
    {
        this.skillName = skillName;
        this.skillDamage = skillDamage;
        this.skillSprite = skillSprite;
        this.skillDescription = skillDescription;
        this.skillType = skillType;
        this.skillAnimPrefab = skillAnimPrefab;
        this.skillAnimationClipName = skillAnimationClipName;
    }
}
