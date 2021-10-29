using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    class OnStunStarted : UnityEvent { }
    [SerializeField] OnStunStarted sig_onStunStarted;

    [System.Serializable]
    class OnStunEnded : UnityEvent { }
    [SerializeField] OnStunEnded sig_onStunEnded;

    float m_remainingStunSeconds = 0.0f; //Can be negative!

    Rigidbody m_rigidBody;

    Vector3 m_velocity = Vector3.zero;
    
    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        Debug.Assert(m_rigidBody != null, "Enemigo sin rigidBody");
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(m_remainingStunSeconds > 0.0f)
        {
            m_remainingStunSeconds -= Time.deltaTime;
            if(m_remainingStunSeconds <= 0.0f)
            {
                //Señal de recover
                sig_onStunEnded.Invoke();
            }
        }
        else
        {
            m_rigidBody.velocity = m_velocity;
        }
       
    }

    public void OnLifeChanged(uint i_old, uint i_new)
    {
        if (i_old > i_new) //Me han hecho daño
        {
            //Stun!
            m_remainingStunSeconds = 0.8f;
            //Señal de Stun!
            sig_onStunStarted.Invoke();
        }
    }

    public void SetVelocity(Vector3 i_velocity)
    {
        m_velocity = i_velocity;
    }
}
