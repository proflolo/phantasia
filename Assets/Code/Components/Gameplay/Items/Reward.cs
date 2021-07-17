using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    World m_gameWorld;
    bool m_rewardGiven = false;
    [SerializeField] ItemDef m_itemToReward;
    [SerializeField] uint m_itemAmount = 1;

    public void Awake()
    {
        Debug.Assert(m_itemToReward != null, "No hay item para el chest");
        Debug.Assert(m_itemAmount > 0, "No puedes dar 0 (o menos) de objetos en un cofre");
        m_gameWorld = GetComponentInParent<World>();
    }

    public void OnAction()
    {
        if (!m_rewardGiven)
        {
            m_gameWorld.RequestAddItem(m_itemToReward, m_itemAmount);
            m_rewardGiven = true;
        }
    }
}
