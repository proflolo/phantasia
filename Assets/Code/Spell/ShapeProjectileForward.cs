using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeProjectileForward : Shape
{
    GameObject m_template;
    IList<Effect> m_onImpactEffect;

    public ShapeProjectileForward(GameObject i_template, IList<Effect> i_impactEffects)
    {
        m_template = i_template;
        m_onImpactEffect = i_impactEffects;
    }

    public override void CreateFor(Params i_params)
    {
        GameObject rawObject = GameObject.Instantiate(m_template, i_params.position, Quaternion.identity, i_params.world.transform);
        SpellMovementForward forward = rawObject.AddComponent<SpellMovementForward>();
        forward.Configure(this, i_params.caster, i_params.normal);
    }

    public IList<Effect> GetOnImpactEffects()
    {
        return m_onImpactEffect;
    }
    public override uint ComputeCastCost()
    {
        uint cost = 0;
        foreach (Effect effect in m_onImpactEffect)
        {
            cost += effect.ComputeCastCost();
        }

        return cost;
    }
}
