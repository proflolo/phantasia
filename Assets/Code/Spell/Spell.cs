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
        m_forgeCost = 0;
        m_castCost = 0;
        foreach(Effect effect in i_initialEffects)
        {
            m_castCost += effect.ComputeCastCost();
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
