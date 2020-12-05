using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharState
{
    ICharState DoState(CharStateManager thisCharacter);
    void InitState(CharStateManager thisCharacter);
    void ExitState(CharStateManager thisCharacter);
}
