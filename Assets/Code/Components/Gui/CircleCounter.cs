using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CircleCounter : MonoBehaviour
{
    Text m_text;
    UIDirector m_director;

    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_director = GetComponentInParent<UIDirector>();
        Debug.Assert(m_director != null, "No encuentro Direcdtor de UI");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
