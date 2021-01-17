using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfflictionStatus : MonoBehaviour
{
    [SerializeField] CharSelectPortrait charSelectPortrait;
    enum AfflictionType { Attack, Defense, Health };
    [SerializeField] AfflictionType afflictType;
    UnitStats playersStats;

    private void Start()
    {
        playersStats = charSelectPortrait.GetOwner().GetComponent<UnitStats>();
        if (charSelectPortrait != null)
        {
            Debug.Log($"This Affliction Statuts objects owner is : {charSelectPortrait.GetCharName()}", this);
        }
        switch (afflictType)
        {
            case AfflictionType.Attack:
                playersStats.attackIsUp += MakeVisible;
                break;
            case AfflictionType.Defense:
                playersStats.defenseIsUp += MakeVisible;
                break;
            case AfflictionType.Health:
                playersStats.healthIsUp += MakeVisible;
                break;
        }
    }

    public void MakeVisible(bool toggle = true)
    {
        this.gameObject.GetComponent<Image>().enabled = toggle;
    }

}
