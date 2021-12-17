using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BattleMCMove : IBattleMCState
{
    Vector2 m_rawAxis;
    Vector2 m_velocity;
    Vector2 m_impactVelocity;
    
    Rigidbody m_rigidBody;

    public BattleMCMove(Rigidbody i_rigidBody)
    {
        m_rigidBody = i_rigidBody;
    }

    public void OnMove(InputValue i_value)
    {
        m_rawAxis = i_value.Get<Vector2>();
        m_velocity = new Vector3(m_rawAxis.x * GameplayConstants.humanSpeed, 0.0f, 0.0f);
    }

    public void Start()
    {
        m_rawAxis = Vector2.zero;
    }

    public IBattleMCState.Result Update()
    {
        m_rigidBody.velocity = m_velocity;
        return IBattleMCState.Result.Busy;
    }
}
