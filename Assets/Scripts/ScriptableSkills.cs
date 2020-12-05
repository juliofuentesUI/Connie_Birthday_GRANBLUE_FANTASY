using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Skill")]
public class ScriptableSkills : ScriptableObject
{
    #pragma warning disable 0649
    [SerializeField] string skillName;
    [SerializeField] string skillAnimationClipName;
    [SerializeField] float skillDamage;
    [SerializeField] Sprite skillSprite;
    [SerializeField] string skillDescription;
    [SerializeField] string skillType;
    [SerializeField] GameObject skillAnimPrefab;
    #pragma warning restore 0649
    public string GetSkillName() => skillName;
    public string GetSkillAnimationName() => skillAnimationClipName;
    public float GetSkillDamage() => skillDamage;
    public Sprite GetSkillSprite() => skillSprite;
    public string GetSkillDescription() => skillDescription;
    public string GetSkillType() => skillType;
    public GameObject GetSkillAnimPrefab() => skillAnimPrefab;
    public SkillObject GetSkillObject()
    {
        return new SkillObject(this.skillName, this.skillDamage, this.skillSprite, this.skillDescription,
            this.skillType, this.skillAnimPrefab, this.skillAnimationClipName);
    }

}
