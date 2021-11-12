using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ManaComponent))]
[RequireComponent(typeof(Rigidbody))]
public class BattleMCController : MonoBehaviour
{
    [SerializeField] GameObject dummyProjectile;
    [SerializeField] GameObject dummyExplosion;

    BattleWorld m_battleWorld;
    ManaComponent m_manaComponent;
    Rigidbody m_rigidBody;
    Vector2 m_rawAxis;
    Vector2 m_velocity;

    private void Awake()
    {
        m_battleWorld = GetComponentInParent<BattleWorld>();
        m_manaComponent = GetComponent<ManaComponent>();
        m_rigidBody = GetComponent<Rigidbody>();
        Debug.Assert(dummyProjectile != null, "No tenemos dummy Projectile");
        Debug.Assert(dummyExplosion != null, "No tenemos dummy Explosion");
        Debug.Assert(m_manaComponent != null, "No tenemos Componente de Mana");
        Debug.Assert(m_rigidBody != null, "No tenemos Componente de RigidBody");
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rawAxis = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidBody.velocity = m_velocity;
    }

    void OnCastSpell(InputValue i_value)
    {
        if (i_value.isPressed)
        {
            //Lanzar el hechizo

            

            Spell trainingSpell = m_battleWorld.GetTrainingSpell();
            if(trainingSpell != null)
            {
                uint cost = trainingSpell.GetCastCost();
                if(cost <= m_manaComponent.GetCurrentMana())
                {
                    CastSpell(trainingSpell);
                    m_manaComponent.ApplyConsumption(cost);
                }
            }
            else
            {
                List<Effect> explosionEffects = new List<Effect>();
                explosionEffects.Add(new EffectDamage(1));
                
                ShapeExplosion explosion = new ShapeExplosion(dummyExplosion, explosionEffects, 1.0f, 3.0f);
                
                List<Effect> onImpactEffects = new List<Effect>();
                onImpactEffects.Add(new EffectSpawn(explosion));
                ShapeProjectileForward projectile = new ShapeProjectileForward(dummyProjectile, onImpactEffects);
                EffectSpawn spawn = new EffectSpawn(projectile);
                
                List<Effect> initialeffects = new List<Effect>();
                initialeffects.Add(spawn);
                
                Spell spell = new Spell(initialeffects);
                CastSpell(spell);
            }
        }
    }
    void OnMove(InputValue i_value)
    {
        m_rawAxis = i_value.Get<Vector2>();
        m_velocity = new Vector3(m_rawAxis.x * GameplayConstants.humanSpeed, 0.0f, 0.0f);
        
    }
    void CastSpell(Spell i_spell)
    {
        EffectHandler.Cast(i_spell, gameObject, m_battleWorld, transform.position + Vector3.up * 1.0f, Vector3.right);
    }
}
