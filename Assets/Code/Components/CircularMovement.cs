using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] float m_radiusMeters = 2;
    [SerializeField] float m_circleSeconds = 10;


    public struct Circlestate
    {
        public int numCircles;
    }
    [System.Serializable] class NumcirclesChangedEvent : UnityEvent<Circlestate> { }
    [SerializeField] NumcirclesChangedEvent sig_circlesChanged;

    World m_world;

    private int m_numCircles = 0;

    Vector3 m_initialPosition;
    float m_ellapsedSeconds = 4;

    private void Awake()
    {
        m_world = GetComponentInParent<World>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_numCircles = 0;
        m_initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_ellapsedSeconds += Time.deltaTime;
        gameObject.transform.position = m_initialPosition
                                            + gameObject.transform.up * Mathf.Sin(2 * Mathf.PI * m_ellapsedSeconds / m_circleSeconds) * m_radiusMeters
                                            + gameObject.transform.right * Mathf.Cos(2 * Mathf.PI * m_ellapsedSeconds / m_circleSeconds) * m_radiusMeters;

        int oldValue = m_numCircles;
        m_numCircles = (int)(m_ellapsedSeconds / m_circleSeconds);
        if (oldValue != m_numCircles)
        {
            Circlestate state = new Circlestate();
            state.numCircles = m_numCircles;
            sig_circlesChanged.Invoke(state);
            m_world.OnNumCirclesChanged(this, m_numCircles);
        }

    }

    public int GetNumCircles()
    {
        return m_numCircles;
    }
}
