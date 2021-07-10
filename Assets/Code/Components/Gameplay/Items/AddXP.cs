using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddXP : MonoBehaviour
{
    bool m_rewardGiven = false;
    World m_gameWorld;


    private void Awake()
    {
        m_gameWorld = GetComponentInParent<World>();
        Debug.Assert(m_gameWorld);
    }

    public void OnAction()
    {
        if (!m_rewardGiven)
        {
            m_gameWorld.RequestAddXP(10);
            m_rewardGiven = true;
        }
    }
}
