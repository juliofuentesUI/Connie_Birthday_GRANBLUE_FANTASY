using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

// I want this class to be responsible for animating INTO charCommand panels
// and manipulating the carousel of charCommandPanels.
public class UI_AnimController : MonoBehaviour
{
    #pragma warning disable 0649
    GameManager gameManager;
    GameObject[] charCommandPanels;
    GameObject[] partySelectPortraits;
    GameObject curCharPortrait;
    GameObject commandCarousel;
    Vector3 carouselOriginPos;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject affinityChart;
    //Serialize curCharIndex for debugging purposes.
    [SerializeField] int curCharIndex;
    //tmpro text field access
    private int totalCharsLength;
    private Moveable moveScript;
    private bool currentlyAnimating;
    [SerializeField] string currentSelectedSkill;
    [SerializeField] SkillData currentSelectedSkillObject;
    [SerializeField] CharStateManager currentSelectedSkillOwner;
    [SerializeField] Image textBubbleSkillCostWarning;
    [SerializeField] TextMeshProUGUI characterNameTextField;
    #pragma warning restore 0649
    private void Awake()
    {
        partySelectPortraits = GameObject.FindGameObjectsWithTag("PartySelectPortrait");
        gameManager = FindObjectOfType<GameManager>();
        commandCarousel = GameObject.FindWithTag("CommandCarousel");
        carouselOriginPos = commandCarousel.transform.position;
        totalCharsLength = gameManager.heroConfig.Count;
        moveScript = commandCarousel.GetComponent<Moveable>();
        Debug.Log($"carouseloriginPos.y : {carouselOriginPos.y}");
        Debug.Log($"carouseloriginPos.x : {carouselOriginPos.x}");
        //each charCommandPanels has a public int prop called charSelectIndex, now i can dynamically access them.
    }

    public void ProcessClickRelay(PointerEventData eventData)
    {
        //print($"eventData.selectedObject.tag : {eventData.selectedObject.tag}");
        if (currentlyAnimating == true || !eventData.selectedObject) return;
        switch(eventData.selectedObject.tag)
        {
            case "PartySelectPortrait":
                ShowCharCommandUI(eventData);
                break;
            case "LeftButton":
                MoveActivePanels(eventData);
                break;
            case "RightButton":
                MoveActivePanels(eventData);
                break;
            case "BackButton":
                ShowPartySelect();
                break;
            case "Skill":
                ShowSkillInfo(eventData);
                break;
            case "AttackButton":
                RelayAttack(eventData);
                break;
            default:
                break;
        }
    }

    private void RelayAttack(PointerEventData eventData)
    {
        if (currentSelectedSkill == "") return;
        //this isDisabled line may not even have to run because...were going to block it from coming into ui_anim at all.
        if (currentSelectedSkillObject.IsDisabled()) return;
        CharStateManager owner = currentSelectedSkillOwner;
        //check first with that owner.skillCost > 0
        //if so, owner.InitAttack(currentSelectedSKill), then disableSkill() and resetSkillSelection(); and owner skillCost--;
        if (owner.skillCostRemaining > 0)
        {
            owner.InitAttack(currentSelectedSkill);
            currentSelectedSkillObject.DisableSkill();
            ResetCurrentSkillSelection();
        }
        else
        {
            //ELSE that means we are out of skills. so invoke an "OUT OF SKILL POINTS" event that the MASTER ANIMATOR listens to.
            //after invoking it, disable that last un-usable skill via currentSleected disablskill() and ResetCurrentSkillSellection()
            //now all skills are un-interactable.!
            //invoke OUT OF SKILL POINTS method here.
            StartCoroutine(ShowSkillCostDepletedBubble());
            currentSelectedSkillObject.DisableSkill();
            ResetCurrentSkillSelection();
        }
        //remember to re-enable skills with TurnSystem.CS
    }

    private void SetCurrentSkillSelected(SkillData skillData)
    {
        currentSelectedSkill = skillData.GetSkillName();
        currentSelectedSkillObject = skillData;
        currentSelectedSkillOwner = skillData.GetOwner();
    }

    private void ResetCurrentSkillSelection()
    {
        currentSelectedSkill = "";
        currentSelectedSkillObject = null;
        currentSelectedSkillOwner = null;
    }

    private void ShowSkillInfo(PointerEventData eventData)
    {
        // this means a skill has been casted. PUT IT IN THE QUEUE 
        // if they press ATTACK , then we relay to manager.
        // and disable the skill btw.
        //we should cache this but fuck it no time.
        SkillData skillData = eventData.selectedObject.GetComponent<SkillData>();
        skillData.SetTextSkillName();
        skillData.SetTextSkillDescription();
        SetCurrentSkillSelected(skillData);
        //we must set a variable globally to always know whcih skill was selected. OH and everytime a panel moves left or right. nullify it.
    }

    private void IsAnimating(bool isAnimating)
    {
        currentlyAnimating = isAnimating;
    }

    private void ShowPartySelect()
    {
        //basically when you press the BACK BUTTON
        setVisible_AffinityChart(true);
        setVisible_BackButton(false);
        ToggleArrows();
        ToggleCommandPanels();
        ClearCharacterNameText();
        foreach(var charPortrait in partySelectPortraits)
        {
            charPortrait.SetActive(true);
            Debug.Log("We're about to make .isSelected on all owners false");
            charPortrait.GetComponent<CharSelectPortrait>().GetOwner().isSelected = false;
            //by setting the owner to false. we ensure that they all return to idleINACTIVE states.
        }
    }

    private void ShowCharacterName()
    {
        //shows the currently selected characters name
        string charName = partySelectPortraits[curCharIndex].GetComponent<CharSelectPortrait>().GetCharName();
        characterNameTextField.text = charName;
        //grab hard referenced text box to show name.
    }

    private void ClearCharacterNameText()
    {
        characterNameTextField.text = "";
    }

    private void MoveActivePanels(PointerEventData eventData)
    {
        Vector3 curPos = commandCarousel.transform.position;
        //this currentSelectedSkill is a very dangerous but necessary line of code..clean it up later
        //its responsible for a lot
        //WE HAVE TO ALSO SELECT THE CURRENT OWNER WHEN WE PRESS LEFT AND RIGHT ON THESE PANELS
        if (eventData.selectedObject.tag == "LeftButton")
        {
            if (curCharIndex - 1 < 0) return;
            ResetCurrentSkillSelection();
            partySelectPortraits[curCharIndex].GetComponent<CharSelectPortrait>().GetOwner().isSelected = false;
            curCharIndex--;
            partySelectPortraits[curCharIndex].GetComponent<CharSelectPortrait>().GetOwner().isSelected = true;
            Vector3 destination = new Vector3(curPos.x + Screen.width, curPos.y, curPos.z);
            IsAnimating(true);
            ShowCharacterName();
            moveScript.MoveTo(destination, () => IsAnimating(false));
        }

        if (eventData.selectedObject.tag == "RightButton")
        {
            if (curCharIndex + 1 >= totalCharsLength) return;
            ResetCurrentSkillSelection();
            partySelectPortraits[curCharIndex].GetComponent<CharSelectPortrait>().GetOwner().isSelected = false;
            curCharIndex++;
            partySelectPortraits[curCharIndex].GetComponent<CharSelectPortrait>().GetOwner().isSelected = true;
            Vector3 destination = new Vector3(curPos.x - Screen.width, curPos.y, curPos.z);
            IsAnimating(true);
            ShowCharacterName();
            moveScript.MoveTo(destination, () => IsAnimating(false));
        }
    }

    private void ShowCharCommandUI(PointerEventData selectedChar)
    {
        curCharPortrait = selectedChar.selectedObject;
        curCharIndex = curCharPortrait.GetComponent<CharSelectPortrait>().charSelectIndex;
        CharStateManager owner = curCharPortrait.GetComponent<CharSelectPortrait>().GetOwner();
        owner.isSelected = true;
        RectTransform rectTransform = curCharPortrait.GetComponent<RectTransform>();
        foreach(var charPortrait in partySelectPortraits)
        {
            if (charPortrait != curCharPortrait)
                charPortrait.SetActive(false);
        }
        setVisible_AffinityChart(false);
        setVisible_BackButton(true);
        Vector3 targetPosition = gameManager.targetPortraitPosition.Value;
        MoveInactiveCommandPanels(curCharIndex);
        IsAnimating(true);
        ShowCharacterName();
        curCharPortrait.GetComponent<Moveable>().MoveTo(targetPosition, ToggleCommandPanels, ToggleArrows, ResetPortraitPosition, () => IsAnimating(false));
    }

    private void MoveInactiveCommandPanels(int curCharIndex)
    {
        //when inactive, this moves them into position when going from charSelect to cmmdpanel
        Debug.Log($"carouseloriginPos.y : {carouselOriginPos.y}");
        float targetReposition = Screen.width * curCharIndex;
        commandCarousel.transform.position = new Vector3(
                                             carouselOriginPos.x - targetReposition,
                                             carouselOriginPos.y, 0);
        Debug.Log($"carouseloriginPos.y AFTER MOVING : {carouselOriginPos.y}");
        
    }

    private IEnumerator ShowSkillCostDepletedBubble()
    {
        //warn them that their skills are gone.
        //move the warning bubble to be above the cahracter
        //move the bubble to the position of current owner.
        textBubbleSkillCostWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        textBubbleSkillCostWarning.gameObject.SetActive(false);
    }

    private void setVisible_AffinityChart(bool visible)
    {
        affinityChart.SetActive(visible);
    }

    private void setVisible_BackButton(bool visible)
    {
        backButton.SetActive(visible);
    }

    private void ResetPortraitPosition()
    {
        curCharPortrait.transform.position = curCharPortrait.GetComponent<CharSelectPortrait>().originalPosition;
        curCharPortrait.SetActive(false);
    }

    private void ToggleCommandPanels()
    {
        gameManager.allCommandPanels.ForEach((panel) => panel.SetActive(!panel.activeSelf));
    }

    private void ToggleArrows()
    {
        leftArrow.SetActive(!leftArrow.activeSelf);       
        rightArrow.SetActive(!rightArrow.activeSelf);       
    }
}
