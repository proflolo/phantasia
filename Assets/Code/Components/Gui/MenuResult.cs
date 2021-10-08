using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MenuFuture<T>: CustomYieldInstruction
{
    public abstract T GetResult();
}

public class MenuResult<T> : MenuFuture<T>
{
    T m_result;
    bool m_wait = true;

    public MenuResult(T i_defaultValue)
    {
        m_result = i_defaultValue;
    }

    public override bool keepWaiting
    {
        get
        {
            return m_wait;
        }
    }

    public void SetReady(T i_result)
    {
        m_result = i_result;
        m_wait = false;
    }

    public override T GetResult()
    {
        return m_result;
    }
}
