using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler
{
    static public void Cast(Spell i_spell, GameObject i_spellCaster, BattleWorld i_world, Vector3 i_startPoint, Vector3 i_startDirection)
    {
        foreach(Effect effect in i_spell.GetEffects())
        {
            ExecuteEffect(effect, i_spellCaster, i_world, i_startPoint, i_startDirection);
        }
    }

    static public void ExecuteEffect(Effect i_effect, GameObject i_spellCaster, BattleWorld i_world, Vector3 i_startPoint, Vector3 i_startDirection)
    {
        Effect.Params p = new Effect.Params();
        p.caster = i_spellCaster;
        p.world = i_world;
        p.startPoint = i_startPoint;
        p.startDirection = i_startDirection;
        i_effect.ExecuteFor(p);
    }
}
