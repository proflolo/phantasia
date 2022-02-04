using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] Camera m_camera;
    [SerializeField] float m_parallaxRatio = 1.0f;
    Vector3 m_deltaToCamera;
    Vector3 m_initialCameraPosition;
    Vector3 m_myInitialPosition;
    private void Awake()
    {
        Debug.Assert(m_camera != null, "No tenemos cámara en el parallax");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_myInitialPosition = transform.position;
        m_initialCameraPosition = m_camera.transform.position;
        m_deltaToCamera = transform.position - m_camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //si la ratio es 0 -> m_myInitialPosition
        //si la ratio es 1
        //m_camera.transform.position + m_deltaToCamera;
        transform.position = (m_camera.transform.position + m_deltaToCamera) * m_parallaxRatio + m_myInitialPosition * (1.0f - m_parallaxRatio);
    }
}
