using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyVisual : MonoBehaviour
{
    Rigidbody m_rigidbody;
    Animator m_animator;
    SpriteRenderer m_spriteRenderer;
    bool m_stuned = false;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        Debug.Assert(m_animator != null, "Enemigo sin Animator");
        m_rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(m_rigidbody != null, "Enemigo sin rigidBody");
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(m_spriteRenderer != null, "Enemigo sin Sprite!");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_stuned)
        {
            if (m_rigidbody.velocity.x < 0.0f)
            {
                m_spriteRenderer.flipX = false;
            }
            else if (m_rigidbody.velocity.x > 0.0f)
            {
                m_spriteRenderer.flipX = true;
            }
        }

        m_animator.SetFloat("Speed", m_rigidbody.velocity.magnitude);
    }

    public void OnStunStarted()
    {
        m_animator.SetTrigger("Hit");
        m_stuned = true;
    }

    public void OnStunEnded()
    {
        m_animator.SetTrigger("Recover");
        m_stuned = false;
    }
}
