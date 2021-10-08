using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    IList<Effect> m_effects;
    uint m_castCost;
    uint m_forgeCost;
    

    public Spell(IList<Effect> i_initialEffects)
    {
        m_effects = i_initialEffects;
        //Coste de lanzamiento
    }

    public Spell(IList<RuneDef> i_runes)
    {
        //magia de la forja
        //Contaremos los costes (forja y lanzamiento)
        m_forgeCost = 0;
        m_castCost = 0;
        foreach (RuneDef rune in i_runes)
        {
            m_forgeCost += rune.forgeCost;
        }
    }

    public IList<Effect> GetEffects()
    {
        return m_effects;
    }

    public uint GetForgeCost()
    {
        return m_forgeCost;
    }

    public uint GetCastCost()
    {
        return m_castCost;
    }
}
