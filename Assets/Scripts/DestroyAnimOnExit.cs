using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimOnExit : StateMachineBehaviour
{ 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"CurrentState length is : {stateInfo.length}");
        Destroy(animator.gameObject, stateInfo.length);
    }
}

