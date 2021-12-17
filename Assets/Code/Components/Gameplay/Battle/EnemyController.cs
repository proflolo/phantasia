using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : CharacterController
{
    [System.Serializable]
    class OnStunStarted : UnityEvent { }
    [SerializeField] OnStunStarted sig_onStunStarted;

    [System.Serializable]
    class OnStunEnded : UnityEvent { }
    [SerializeField] OnStunEnded sig_onStunEnded;

    [System.Serializable]
    class OnAttackStarted : UnityEvent { }
    [SerializeField] OnAttackStarted sig_onAttackStarted;

    [System.Serializable]
    class OnAttackEnded : UnityEvent { }
    [SerializeField] OnAttackEnded sig_onAttackEnded;


    float m_remainingStunSeconds = 0.0f; //Can be negative!

    Rigidbody m_rigidBody;

    Vector3 m_velocity = Vector3.zero;

    float m_attackingSeconds = 0.0f;
    
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

        if(m_attackingSeconds > Time.deltaTime)
        {
            m_attackingSeconds -= Time.deltaTime;
            if(m_attackingSeconds < Time.deltaTime)
            {
                sig_onAttackEnded.Invoke();
            }
        }
        else
        {
            m_attackingSeconds = 0.0f;
        }
       
        if(m_velocity.x > 0.001f) //derecha
        {
            transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.up);
        }
        else if (m_velocity.x < -0.001f)//izquierda
        {
            transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.up);
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

    public float walkSpeed
    {
        get
        {
            return 2.0f;
        }
    }

    public float runSpeed
    {
        get
        {
            return 5.0f;
        }
    }

    public void PerformAttack()
    {
        if(m_attackingSeconds < 0.001f)
        {
            sig_onAttackStarted.Invoke();
        }
        m_attackingSeconds = 0.5f;
    }

    public bool IsAttacking()
    {
        return m_attackingSeconds > 0.001f;
    }

    public override void ApplyImpact(Vector3 i_direction, float i_force)
    {
        
    }
}
