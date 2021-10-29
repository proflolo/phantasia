using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ManaComponent))]
public class BattleMCController : MonoBehaviour
{
    [SerializeField] GameObject dummyProjectile;
    [SerializeField] GameObject dummyExplosion;

    BattleWorld m_battleWorld;
    ManaComponent m_manaComponent;

    private void Awake()
    {
        m_battleWorld = GetComponentInParent<BattleWorld>();
        m_manaComponent = GetComponent<ManaComponent>();
        Debug.Assert(dummyProjectile != null, "No tenemos dummy Projectile");
        Debug.Assert(dummyExplosion != null, "No tenemos dummy Explosion");
        Debug.Assert(m_manaComponent != null, "No tenemos Componente de Mana");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void CastSpell(Spell i_spell)
    {
        EffectHandler.Cast(i_spell, gameObject, m_battleWorld, transform.position + Vector3.up * 1.0f, Vector3.right);
    }
}
