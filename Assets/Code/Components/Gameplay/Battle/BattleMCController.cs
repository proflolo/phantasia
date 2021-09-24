using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleMCController : MonoBehaviour
{
    [SerializeField] GameObject dummyProjectile;
    [SerializeField] GameObject dummyExplosion;

    BattleWorld m_battleWorld;

    private void Awake()
    {
        m_battleWorld = GetComponentInParent<BattleWorld>();
        Debug.Assert(dummyProjectile != null, "No tenemos dummy Projectile");
        Debug.Assert(dummyExplosion != null, "No tenemos dummy Explosion");
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
            //TODO
            List<Effect> explosionEffects = new List<Effect>();
            explosionEffects.Add(new EffectDamage());
            //ShapeProjectileForward bouncedProjectile = new ShapeProjectileForward(dummyProjectile, onImpactEffects);
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

    void CastSpell(Spell i_spell)
    {
        EffectHandler.Cast(i_spell, gameObject, m_battleWorld, transform.position + Vector3.up * 1.0f, Vector3.right);
    }
}
