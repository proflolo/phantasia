using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaComponent : MonoBehaviour
{
    [SerializeField] uint m_maxMana;
    uint m_currentMana;

    private void Awake()
    {
        Debug.Assert(m_maxMana > 0, "Los HP de un enemigo no pueden ser 0");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentMana = m_maxMana;
    }

    public void ApplyConsumption(uint i_mana)
    {
        if (i_mana >= m_currentMana)
        {
            m_currentMana = 0;
            
        }
        else
        {
            m_currentMana -= i_mana;
        }
    }

    public uint GetCurrentMana()
    {
        return m_currentMana;
    }
}
