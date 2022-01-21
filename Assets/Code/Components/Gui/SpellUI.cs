using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class SpellUI : LocalizationVariable
{
    Spell m_spell;

    

    public string cost
    {
        get
        {
            if(m_spell == null)
            {
                return "0";
            }
            else
            {
                //return m_spell.GetCastCost();
                uint value = 123456;
                return value.ToString(CultureInfo.CurrentUICulture.NumberFormat);
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
