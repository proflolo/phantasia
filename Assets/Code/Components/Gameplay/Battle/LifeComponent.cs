using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeComponent : MonoBehaviour
{
    [SerializeField] uint m_maxHP;
    uint m_currentHP;

    [System.Serializable]
    class OnLifeChanged : UnityEvent<uint /*old*/, uint /*new*/> { }

    [SerializeField] OnLifeChanged sig_onInteracted;

    private void Awake()
    {
        Debug.Assert(m_maxHP > 0, "Los HP de un enemigo no pueden ser 0");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentHP = m_maxHP;
    }

    public void ApplyDamage(uint i_damage)
    {
        if (i_damage >= m_currentHP)
        {
            m_currentHP = 0;
            //Destroy
            Destroy(gameObject);
        }
        else
        {
            uint original = m_currentHP;
            m_currentHP -= i_damage;
            sig_onInteracted.Invoke(original, m_currentHP);
        }
    }
}
