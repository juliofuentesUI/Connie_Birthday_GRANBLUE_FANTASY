using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this must receive values from previous scene to instantiate the correct heroes in carousel
    #pragma warning disable 0649
    [SerializeField] GameObject charCommandPanel;
    [SerializeField] GameObject[] partySelectPortraits;
    [SerializeField] Transform carouselCommandPanel;
    //heroConfig is hardcoded for now. make sure it isn't after
    [SerializeField] public List<ScriptableHero> heroConfig;
    public List<GameObject> allCommandPanels;
    public static Vector3 bossPosition { get; private set; }
    [SerializeField] GameObject bossInstance;
    public Vector3? targetPortraitPosition { get; private set; }
    [SerializeField] public List<CharStateManager> currentCharacters;
    [SerializeField] AudioClip BattleQuestBG01;
    #pragma warning restore 0649

    private void Awake()
    {
        //step 1. Stub in portrait details and name for portrait.
        //in the meantime, just use these dummy images.
        bossPosition = bossInstance.GetComponent<Transform>().position;
    }

    private void Start()
    {
        InitializeGame();
        SoundManager.Instance.PlayMusic(BattleQuestBG01);
    }

    public void AddCharacterToList(CharStateManager newCharacter)
    {
        if (newCharacter != null)
        {
            currentCharacters.Add(newCharacter);
        }
    }


    private void InitializeGame()
    {
        //step 1. Stub in portrait details and name for portrait.
        //in the meantime, just use these dummy images.
        GameObject curCharPanel = null;
        ScriptableSkills[] skillsConfig = null;
        CharSelectPortrait curCharPortrait = null;
        SkillData[] curSkillsInPanel = null;
        GameObject curHero;
        for (int i = 0; i < heroConfig.Count; i++)
        {
            partySelectPortraits[i].GetComponent<CharSelectPortrait>().SetPortraitImage(heroConfig[i].GetCharSprite());
            partySelectPortraits[i].GetComponent<CharSelectPortrait>().SetCharName(heroConfig[i].GetCharName());
            partySelectPortraits[i].GetComponent<CharSelectPortrait>().SetCharIndex(i);
            //while your here. INSTANTIATE the commandPanels too but setActive to false. We just want them in memory.
            //the hero panels need. Portrait, 3 text skill icons. and appropriate text to go along with those skiills. 
            if (!curCharPanel)
            {
                //make sure the portrait from the commandPanel is aligned with the already existing partySelect portraits.
                //portraitPos_Y is STRICTLY for alignment!  it will guarantee that from tnow on all panels will be aligned with
                //the party select portraits!
                curCharPanel = Instantiate(charCommandPanel, carouselCommandPanel);
                allCommandPanels.Add(curCharPanel);
            }
            else
            {
                //curCharPanel will reference the LAST instantiated position.x, so we just do that + Screen.width to get optimal spacing for carousel
                Vector3 newPosition = new Vector3(curCharPanel.transform.position.x + Screen.width, curCharPanel.transform.position.y, 0);
                curCharPanel = Instantiate(charCommandPanel, newPosition, Quaternion.identity, carouselCommandPanel);
                allCommandPanels.Add(curCharPanel);
            }
            //instantiate a panel as a child of carousel. then were going to stub it in with data.
            curCharPortrait = curCharPanel.GetComponentInChildren<CharSelectPortrait>();
            curSkillsInPanel = curCharPanel.GetComponentsInChildren<SkillData>();

            //YOU'RE GETTING AN ERROR CAUSE THIS IS HAPPENING WHILE ITS DIS-ACTIVE!!
            //YOU CAN'T GETCOMPONENT ON A DE-ACTIVATED COMPONENT.
            //THIS IS THE PORTRAIT IMAGE ON THE CMMD PANEL!! 
            curCharPortrait.SetPortraitImage(heroConfig[i].GetCharSprite());
            curCharPortrait.SetCharName(heroConfig[i].GetCharName());
            curCharPortrait.SetCharIndex(i);

            //if error, check where hero is instantiated.
            curHero = Instantiate(heroConfig[i].GetHeroPrefab(), heroConfig[i].GetSpawnPosition(), Quaternion.identity);
            //curHero = Instantiate(heroConfig[i].GetHeroPrefab());
            CharStateManager charStatemanager = curHero.GetComponent<CharStateManager>();
            partySelectPortraits[i].GetComponent<CharSelectPortrait>().SetOwner(charStatemanager);
            //experimental line below
            curCharPortrait.SetOwner(charStatemanager);
            //experimental line above
            skillsConfig = heroConfig[i].GetCharSkills();
            for (int j = 0; j < skillsConfig.Length; j++)
            {
                curSkillsInPanel[j].SetSkillSprite(skillsConfig[j].GetSkillSprite());
                curSkillsInPanel[j].SetSkillName(skillsConfig[j].GetSkillName());
                curSkillsInPanel[j].SetSkillDescription(skillsConfig[j].GetSkillDescription());
                curSkillsInPanel[j].SetOwner(charStatemanager);
                //now add thisSKills data to our curreent heros stateManager
                charStatemanager.skillDictionary.Add(skillsConfig[j].GetSkillName(), skillsConfig[j].GetSkillObject());
            }

            //now that the UI is all setup. Instantiate this hero!

            //THE LINE OF CODE BELOW ONLY WORKS ONCE! we just want taht ONE target position on any screen once its instantiated.
            //what this allows me to do is that if the screen is wide, we'll still get the appropriate position to animate to
            targetPortraitPosition = targetPortraitPosition.HasValue == false 
                                     ? curCharPortrait.transform.position
                                     : targetPortraitPosition.Value;
            curCharPanel.SetActive(false);
        }
    }
}
