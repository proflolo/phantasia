using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellExplosion : SpellBehaviour
{
    ShapeExplosion m_shapeDef;
    BattleWorld m_world;
    GameObject m_caster;
    Vector3 m_launchDirection;
    

    float m_ellapsedSeconds = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        _Awake();
        m_world = GetComponentInParent<BattleWorld>();
    }

    // Update is called once per frame
    void Update()
    {
        m_ellapsedSeconds += Time.deltaTime;
        if (m_ellapsedSeconds > m_shapeDef.durationInSeconds)
        {
            ScheduleDestruction();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void Configure(ShapeExplosion i_shapeDef, GameObject i_caster, Vector3 i_launchDirection)
    {
        m_shapeDef = i_shapeDef;
        m_caster = i_caster;
        m_launchDirection = i_launchDirection.normalized;
    }

    void OnCollisionEnter(Collision i_collision)
    {
        foreach (Effect effect in m_shapeDef.GetOnImpactEffects())
        {
            EffectHandler.ExecuteEffect(effect, m_caster, m_world, i_collision.contacts[0].point, i_collision.contacts[0].normal, i_collision.gameObject);
        }
    }

    
}
