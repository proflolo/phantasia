using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawn : Effect
{
    Shape m_shape;
    public EffectSpawn(Shape i_shape)
    {
        m_shape = i_shape;
    }

    public override void ExecuteFor(Params i_params)
    {
        Shape.Params effectParams;
        effectParams.caster = i_params.caster;
        effectParams.world = i_params.world;
        effectParams.position = i_params.startPoint;
        effectParams.normal = i_params.startDirection;
        m_shape.CreateFor(effectParams);
    }
}
