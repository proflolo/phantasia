using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleMCController))]
public class HumanoidBattleVisual : HumanoidVisual
{
    private float m_idleSeconds = 0.0f;
    BattleMCController m_battleCharacter;
    [SerializeField]
    float m_lookRightAngle = 140;

    [SerializeField]
    float m_lookLeftAngle = 220;

    private void Awake()
    {
        AwakeImpl();
        m_battleCharacter = GetComponent<BattleMCController>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateImpl();
        Vector3 velocity = m_rigidBody.velocity;
        float speedMagnitude = velocity.magnitude;
        if(speedMagnitude < 0.01f)
        {
            m_idleSeconds += Time.deltaTime;
            if(m_idleSeconds > 10.0f)
            {
                //Lanzamos el evento de break
                int nextBreak = Random.Range(0, 2);
                if(nextBreak == 0)
                {
                    m_animator.SetTrigger("Break1");
                }
                else
                {
                    m_animator.SetTrigger("Break2");
                }
                m_idleSeconds = 0.0f;
            }
        }
        else
        {
            m_idleSeconds = 0.0f;
        }

        bool isStunned = m_battleCharacter.IsStunned();

        if (speedMagnitude > 0.01f && !isStunned)
        {
            //rotar personaje
            if(velocity.x > 2.0f)
            {
                m_rigidBody.transform.rotation = Quaternion.Euler(new Vector3(0.0f, m_lookRightAngle, 0.0f));
            }
            else if (velocity.x < -2.0f)
            {
                m_rigidBody.transform.rotation = Quaternion.Euler(new Vector3(0.0f, m_lookLeftAngle, 0.0f));
            }
        }

            m_animator.SetBool("Stunned", isStunned);
            m_animator.SetFloat("Stun_remain", m_battleCharacter.GetRemainingStunTime());
    }
}
