using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] bool m_limitTransform = false;
    [SerializeField] float m_minX;
    [SerializeField] float m_maxX;

    Vector3 m_delta;

    private void Awake()
    {
        Debug.Assert(m_target != null, "No se ha seteado target para el follow");
    }


    void Start()
    {
        m_delta = transform.position - m_target.position;
    }

    void Update()
    {
        Vector3 finalPosition = m_target.position + m_delta;
        if (m_limitTransform)
        {
            if (finalPosition.x < m_minX)
            {
                finalPosition.x = m_minX;
            }

            if (finalPosition.x > m_maxX)
            {
                finalPosition.x = m_maxX;
            }
        }

        transform.position = finalPosition;


    }
}
