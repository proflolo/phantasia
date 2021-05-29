using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    bool m_rewardGiven = false;

    public void OnAction()
    {
        if (!m_rewardGiven)
        {
            Debug.Log("Objeto entregado");
            m_rewardGiven = true;
        }
    }
}
