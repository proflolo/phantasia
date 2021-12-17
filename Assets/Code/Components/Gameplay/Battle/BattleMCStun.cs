using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleMCStun : IBattleMCState
{
    float m_impactSeconds;
    Rigidbody m_rigidBody;
    Vector3 m_impactVelocity;

    public BattleMCStun(Rigidbody i_rigidBody)
    {
        m_rigidBody = i_rigidBody;
    }

    public void OnMove(InputValue i_value)
    {
        
    }

    public void Start(Vector3 i_direction, float i_force)
    {
        m_impactSeconds = 1.0f;
        m_impactVelocity = i_direction * i_force;
    }

    public IBattleMCState.Result Update()
    {
        m_rigidBody.velocity = m_impactVelocity;
        m_impactSeconds = Mathf.Max(0.0f, m_impactSeconds - Time.deltaTime);
        if(m_impactSeconds > 0.01f)
        {
            return IBattleMCState.Result.Busy;
        }
        else
        {
            return IBattleMCState.Result.Finished;
        }
    }

    public float GetRemainingSAtunTime()
    {
        return m_impactSeconds;
    }
}
