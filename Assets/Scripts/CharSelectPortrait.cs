using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectPortrait : MonoBehaviour
{
    public int charSelectIndex { get; private set; }
    public Vector2 originalPosition;
    public Vector2 originalSize;
    public string characterName { get; private set; }
    private Image image;
    public CharStateManager owner;

    //this script doesn't do much except hold basic information about charSelectPortraits.
    void Awake()
    {
        //self-initialize basic information such as size and position. 
        originalSize = gameObject.GetComponent<RectTransform>().rect.size;
        //originalPosition = gameObject.GetComponent<RectTransform>().transform.position;
        image = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        //self-initialize basic information such as size and position. 
        //were CACHING transform Position in start because in AWAKE() theres a chance itll cache it while its still being
        // repositioned!
        originalPosition = gameObject.GetComponent<RectTransform>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"charPortraitIndex : {charSelectIndex} position : {originalPosition} and size : {originalSize} ", this);
    }

    public void SetOwner(CharStateManager owner)
    {
        this.owner = owner;
    }

    public CharStateManager GetOwner()
    {
        return this.owner;
    }

    public void SetCharIndex(int index)
    {
        this.charSelectIndex = index;
    }

    public void SetCharName(string name)
    {
        characterName = name;
    }

    public string GetCharName()
    {
        return this.characterName;
    }

    public void SetPortraitImage(Sprite sprite)
    {
        image.sprite = sprite;
    }


    private void UnifySizes()
    {
        //ONLY USE THIS if you want to guarantee all objects of type CharPortrait are the same size/height.
        foreach (CharSelectPortrait charPortrait in GameObject.FindObjectsOfType<CharSelectPortrait>())
        {
            charPortrait.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 147.7f);
            charPortrait.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 314.5f);
        }
    }
}
