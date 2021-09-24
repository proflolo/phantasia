using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovementForward : SpellBehaviour
{
    ShapeProjectileForward m_shapeDef;
    BattleWorld m_world;
    GameObject m_caster;
    Vector3 m_launchDirection;
    int m_ignoreInitialCollisions;
    List<Collider> m_collidersToIgnore;
    float m_aliveTime = 0.0f;
   
    // Start is called before the first frame update
    void Awake()
    {
        _Awake();
        m_world = GetComponentInParent<BattleWorld>();
        m_collidersToIgnore = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + m_launchDirection * 0.03f;
        m_aliveTime += Time.deltaTime;
        if(m_aliveTime > 10.0f)
        {
            ScheduleDestruction();
        }
    }

    private void FixedUpdate()
    {
        if (m_ignoreInitialCollisions > 0)
        {
            m_ignoreInitialCollisions--;
        }
    }

    public void Configure(ShapeProjectileForward i_shapeDef, GameObject i_caster, Vector3 i_launchDirection)
    {
        m_ignoreInitialCollisions = 2;
        m_shapeDef = i_shapeDef;
        m_caster = i_caster;
        m_launchDirection = i_launchDirection.normalized;
    }

    void OnCollisionEnter(Collision i_collision)
    {
        if (m_ignoreInitialCollisions > 0)
        {
            m_collidersToIgnore.Add(i_collision.collider);
        }
        else
        {
            if (m_collidersToIgnore.Find((x) => x == i_collision.collider))
            {
                //ignore the collision
            }
            else
            {
                foreach (Effect effect in m_shapeDef.GetOnImpactEffects())
                {
                    EffectHandler.ExecuteEffect(effect, m_caster, m_world, i_collision.contacts[0].point, i_collision.contacts[0].normal);
                }
                //Destroy(gameObject);
                ScheduleDestruction();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_collidersToIgnore.Remove(collision.collider);
    }
}
