using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    IList<Effect> m_effects;

    public Spell(IList<Effect> i_initialEffects)
    {
        m_effects = i_initialEffects;
    }

    public IList<Effect> GetEffects()
    {
        return m_effects;
    }
}
