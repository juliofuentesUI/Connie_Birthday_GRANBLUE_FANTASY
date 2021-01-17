using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBossPortrait : MonoBehaviour
{
    [SerializeField] Sprite overdrivePortrait;
    private void Awake()
    {
        BossStateManager boss = FindObjectOfType<BossStateManager>();
        boss.overdriveStart += ChangePortrait;
    }

    void ChangePortrait()
    {
        GetComponent<Image>().sprite = overdrivePortrait;
    }
}
