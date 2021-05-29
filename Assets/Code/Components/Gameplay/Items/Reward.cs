using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    bool m_rewardGiven = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAction()
    {
        if (!m_rewardGiven)
        {
            Debug.Log("Objeto entregado");
            m_rewardGiven = true;
        }
    }
}
