using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUI : LocalizationVariable
{
    Spell m_spell;

    

    public uint cost
    {
        get
        {
            if(m_spell == null)
            {
                return 0;
            }
            else
            {
                return m_spell.GetCastCost();
            }
        }
    }

    public uint forgeCost
    {
        get
        {
            if (m_spell == null)
            {
                return 0;
            }
            else
            {
                return m_spell.GetForgeCost();
            }
        }
    }

    public Spell spell
    {
        set
        {
            if(m_spell != value)
            {
                m_spell = value;
                sig_onChanged.Invoke();
            }
        }
    }
}
