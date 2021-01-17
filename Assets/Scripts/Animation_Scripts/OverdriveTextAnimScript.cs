using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveTextAnimScript : MonoBehaviour
{
    [SerializeField] BossStateManager boss;
    Animator animator;  

    void Start()
    {
        animator = GetComponent<Animator>();         
        boss.overdriveStart += ShowOverdriveAnim;

    }
    void ShowOverdriveAnim()
    {
        animator.Play("OverdriveText_Anim");
        //change HP portrait.
    }

}
