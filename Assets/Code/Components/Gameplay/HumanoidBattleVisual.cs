using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidBattleVisual : HumanoidVisual
{
    private float m_idleSeconds = 0.0f;

    // Update is called once per frame
    void Update()
    {
        UpdateImpl();
        float speedMagnitude = m_rigidBody.velocity.magnitude;
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


    }
}
