using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFade : MonoBehaviour
{

    [SerializeField] GameObject m_targetObject;
    [SerializeField] float m_maxDistance = 10.0f;
    AudioSource m_source;

    private void Awake()
    {
        m_source = GetComponent<AudioSource>();
        Debug.Assert(m_source != null, "No tenemos audio source");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject i_target)
    {
        m_targetObject = i_target;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_targetObject != null)
        {
            float distance = Vector3.Distance(transform.position, m_targetObject.transform.position);
            float value = Mathf.SmoothStep(0.0f, 1.0f, distance / m_maxDistance);
            m_source.volume = 1.0f - value;
        }
    }
}
