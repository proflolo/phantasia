using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class HumanoidVisual : MonoBehaviour
{
    protected Animator m_animator;
    protected Rigidbody m_rigidBody;
    [SerializeField] protected AudioSource m_stepSource;
    private void Awake()
    {
        AwakeImpl();
        Debug.Assert(m_stepSource != null, "No tenemos step source");
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
        m_lastStepDelta += Time.deltaTime;
    }

    AnimationClip m_lastStepClip = null;
    float m_lastStepDelta = 0.0f;

    protected virtual void PlayStep()
    {
        m_stepSource.pitch = Random.Range(0.85f, 1.15f);
        m_stepSource.Play();
    }

    public void Audio_FootStep(AnimationEvent i_animEvent)
    {
        if(i_animEvent.animatorClipInfo.weight > 0.5f)
        {
            if(m_lastStepClip == i_animEvent.animatorClipInfo.clip)
            {
                PlayStep();
                m_lastStepDelta = 0.0f;

            }
            else
            {
                if(m_lastStepClip == null)
                {
                    PlayStep();
                    m_lastStepDelta = 0.0f;
                }
                else
                {
                    if(m_lastStepDelta > 0.02f)
                    {
                        PlayStep();
                        m_lastStepDelta = 0.0f;
                    }
                    else
                    {

                    }
                }
                m_lastStepClip = i_animEvent.animatorClipInfo.clip;
            }
        }
    }
}
