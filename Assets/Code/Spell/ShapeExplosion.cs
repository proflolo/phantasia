using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeExplosion : Shape
{
    GameObject m_template;
    IList<Effect> m_onImpactEffect;
    float m_durationSeconds;
    float m_radius;

    public ShapeExplosion(GameObject i_template, IList<Effect> i_impactEffects, float i_durationSeconds, float i_radius)
    {
        m_template = i_template;
        m_onImpactEffect = i_impactEffects;
        m_durationSeconds = i_durationSeconds;
        m_radius = i_radius;
    }

    public override void CreateFor(Params i_params)
    {
        GameObject rawObject = GameObject.Instantiate(m_template, i_params.position, Quaternion.identity, i_params.world.transform);
        SpellExplosion forward = rawObject.AddComponent<SpellExplosion>();
        forward.Configure(this, i_params.caster, i_params.normal);
    }

    public IList<Effect> GetOnImpactEffects()
    {
        return m_onImpactEffect;
    }

    public float durationInSeconds
    {
        get
        {
            return m_durationSeconds;
        }
    }

    public float radius
    {
        get
        {
            return m_radius;
        }
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
