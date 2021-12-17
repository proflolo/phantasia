using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class HumanoidVisual : MonoBehaviour
{
    protected Animator m_animator;
    protected Rigidbody m_rigidBody;

    private void Awake()
    {
        AwakeImpl();
    }

    protected void AwakeImpl()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void UpdateImpl()
    {
        float speedMagnitude = m_rigidBody.velocity.magnitude;
        m_animator.SetFloat("Speed", speedMagnitude / GameplayConstants.humanSpeed);
    }
}
