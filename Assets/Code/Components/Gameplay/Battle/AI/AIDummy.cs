using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class AIDummy : MonoBehaviour
{
    EnemyController m_enemyController;
    float m_ellapsedSeconds = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_enemyController = GetComponent<EnemyController>();
        Debug.Assert(m_enemyController != null, "IA sin Enemycontroler");
    }

    // Update is called once per frame
    void Update()
    {
        m_ellapsedSeconds += Time.deltaTime;
        if(m_ellapsedSeconds < 2.0f)
        {
            m_enemyController.SetVelocity(Vector3.left * 2.0f);
        }
        else if (m_ellapsedSeconds < 4.0f)
        {
            m_enemyController.SetVelocity(Vector3.right * 2.0f);
        }
        else
        {
            m_ellapsedSeconds = 0.0f;
        }
    }
}
