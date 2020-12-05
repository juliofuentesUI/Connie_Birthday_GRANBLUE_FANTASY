using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickRelay : MonoBehaviour, IPointerClickHandler
{
    UI_AnimController animController;
    private void Awake()
    {
        animController = FindObjectOfType<UI_AnimController>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //let the UI animator controller know this was clicked. 
        animController.ProcessClickRelay(eventData);
    }
}
