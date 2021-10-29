using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamage : Effect
{
    uint m_damage;
    public EffectDamage(uint i_damage)
    {
        m_damage = i_damage;
    }

    public override void ExecuteFor(Params i_spell)
    {
        if(i_spell.receiver)
        {
            LifeComponent life = i_spell.receiver.GetComponent<LifeComponent>();
            if(life)
            {
                life.ApplyDamage(m_damage);
            }
        }
    }

    public override uint ComputeCastCost()
    {
        return m_damage;
    }



}
