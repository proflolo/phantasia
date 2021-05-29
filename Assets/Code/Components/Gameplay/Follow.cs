using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform m_target;

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
        transform.position = m_target.position + m_delta;
    }
}
