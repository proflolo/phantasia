using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ManaComponent))]
[RequireComponent(typeof(Rigidbody))]
public class BattleMCController : CharacterController
{

    BattleWorld m_battleWorld;
    ManaComponent m_manaComponent;

    IBattleMCState m_currentState;
    BattleMCMove m_moveState;
    BattleMCStun m_stunState;

    Dictionary<IBattleMCState, IBattleMCState> m_onFinishTransitions;

    
    private void Awake()
    {
        m_battleWorld = GetComponentInParent<BattleWorld>();
        m_manaComponent = GetComponent<ManaComponent>();
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Debug.Assert(m_manaComponent != null, "No tenemos Componente de Mana");
        Debug.Assert(rigidBody != null, "No tenemos Componente de RigidBody");

        m_moveState = new BattleMCMove(rigidBody);
        m_stunState = new BattleMCStun(rigidBody);
        m_currentState = m_moveState;
        m_onFinishTransitions = new Dictionary<IBattleMCState, IBattleMCState>();
        m_onFinishTransitions.Add(m_stunState, m_moveState);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_moveState.Start();
    }

    // Update is called once per frame
    void Update()
    {
        IBattleMCState.Result result = m_currentState.Update();
        if(result == IBattleMCState.Result.Finished)
        {
            //¿Qué hago?
            if (m_onFinishTransitions.ContainsKey(m_currentState))
            {
                m_currentState = m_onFinishTransitions[m_currentState];
            }
        }
    }

    void OnCastSpell(InputValue i_value)
    {
        if (i_value.isPressed)
        {
            //Lanzar el hechizo

            

            Spell trainingSpell = m_battleWorld.GetTrainingSpell();
            if(trainingSpell != null)
            {
                uint cost = trainingSpell.GetCastCost();
                if(cost <= m_manaComponent.GetCurrentMana())
                {
                    CastSpell(trainingSpell);
                    m_manaComponent.ApplyConsumption(cost);
                }
            }
            else
            {
                Debug.Assert(false, "To implement dummy spell");
            }
        }
    }
    void OnMove(InputValue i_value)
    {
        m_currentState.OnMove(i_value);
        m_moveState.OnMove(i_value);
    }
    void CastSpell(Spell i_spell)
    {
        EffectHandler.Cast(i_spell, gameObject, m_battleWorld, transform.position + Vector3.up * 1.0f, Vector3.right);
    }

    public override void ApplyImpact(Vector3 i_direction, float i_force)
    {
        m_stunState.Start(i_direction, i_force);
        m_currentState = m_stunState;
    }

    public bool IsStunned()
    {
        return m_currentState == m_stunState;
    }

    public float GetRemainingStunTime()
    {
        if(IsStunned())
        {
            return m_stunState.GetRemainingSAtunTime();
        }
        else
        {
            return 0.0f;
        }
    }
}
