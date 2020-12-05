using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject 
{
    string skillName;
    float skillDamage;
    Sprite skillSprite;
    string skillDescription;
    string skillType;
    GameObject skillAnimPrefab;
    public SkillObject(string skillName, float skillDamage, Sprite skillSprite,
                        string skillDescription, string skillType, GameObject skillAnimPrefab)
    {
        this.skillName = skillName;
        this.skillDamage = skillDamage;
        this.skillSprite = skillSprite;
        this.skillDescription = skillDescription;
        this.skillType = skillType;
        this.skillAnimPrefab = skillAnimPrefab;
    }
}
