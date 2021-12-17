using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBattleMCState
{
    public enum Result
    {
        Busy,
        Finished
    }

    Result Update();

    void OnMove(InputValue i_value);
}
