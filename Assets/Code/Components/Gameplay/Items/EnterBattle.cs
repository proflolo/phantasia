using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBattle : MonoBehaviour
{
    [SerializeField] BattleDef m_battle;
    World m_gameWorld;
    bool m_rewardGiven = false;

    public void Awake()
    {
        m_gameWorld = GetComponentInParent<World>();
    }

    public void OnAction()
    {
        if (!m_rewardGiven)
        {
            m_gameWorld.RequestBattle(m_battle);
            m_rewardGiven = true;
        }
    }
}
