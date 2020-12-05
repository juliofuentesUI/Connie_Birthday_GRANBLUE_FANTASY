using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillData : MonoBehaviour
{
    [SerializeField] string skillName;
    [SerializeField] Sprite skillSprite;
    [SerializeField] string skillDescription;
    [SerializeField] Image imageComponent;
    [SerializeField] bool isDisabled;
    [SerializeField] TextMeshProUGUI textSkillName;
    [SerializeField] TextMeshProUGUI textSkillDescription;
    [SerializeField] CharStateManager owner;
    Button skillButton;


    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        skillButton = GetComponent<Button>();
    }

    public void SetOwner(CharStateManager owner)
    {
        this.owner = owner;
    }

    public CharStateManager GetOwner()
    {
        return owner;
    }

    public void SetSkillName(string skillName)
    {
        this.skillName = skillName;
    }

    public string GetSkillName()
    {
        return skillName;
    }

    public void SetSkillSprite(Sprite skillSprite)
    {
        //a little redundant its ok
        this.skillSprite = skillSprite;
        imageComponent.sprite = this.skillSprite;
    }

    public void SetSkillDescription(string skillDescription)
    {
        this.skillDescription = skillDescription;
    }

    public void SetTextSkillName()
    {
        textSkillName.text = this.skillName;
    }
    public void SetTextSkillDescription()
    {
        textSkillDescription.text = this.skillDescription;
    }

    public void DisableSkill()
    {
        isDisabled = true;
        //make sure to re-enable after boss's turn.
        skillButton.interactable = false;
    }

    public void EnableSkill()
    {
        //ENABLE SKILL WILL BE CALLED AFTER BOSS'S TURN IN TURNSYSTEM.CS!
        isDisabled = true;
        skillButton.interactable = true;
    }

    public bool IsDisabled()
    {
        return isDisabled;
    }
}
