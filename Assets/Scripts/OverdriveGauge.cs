using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverdriveGauge : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] Slider overDriveSlider;

    private void Start()
    {
        if (healthBar != null)
        {
            //were assuming by the time were here,the  values for healthBar will be set
            healthBar.setHealthEvent += SetOverdriveBar;
            overDriveSlider.maxValue = healthBar.slider.maxValue / 2;
        }
    }


    public void SetOverdriveBar(int amount)
    {
        //amount is currentHealth. OD amount is maxHealth - amount
        int overdriveValue = (int) healthBar.slider.maxValue - amount;
        overDriveSlider.value = overdriveValue;
        if (overdriveValue >= overDriveSlider.maxValue)
        {
            //activate ze delegate!!
            //let boss know we've reached overdrive.
            BossStateManager bossState = FindObjectOfType<BossStateManager>();
            bossState.InitOverdrive();
            healthBar.setHealthEvent -= SetOverdriveBar;
            Destroy(this.gameObject, 2.3f);
        }
    }
}
