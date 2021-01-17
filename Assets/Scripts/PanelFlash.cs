using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFlash : MonoBehaviour
{

    [SerializeField] Image panelImageHolder;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float flashSpeedIntro;
    [SerializeField] float flashSpeedOutro;
    Color panelAlphaColor;

    private void Awake()
    {
        panelAlphaColor = panelImageHolder.color;
    }

    private void Update()
    {
        canvasGroup.alpha += Time.deltaTime * flashSpeedIntro;
        if (canvasGroup.alpha >= 1)
        {
            panelAlphaColor.a -= Time.deltaTime * flashSpeedOutro;
            panelImageHolder.color = panelAlphaColor;

            if (panelImageHolder.color.a <= 0)
            {
                Destroy(panelImageHolder.gameObject);
                Destroy(this);
            }
        }
    }

}
