using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleMCController : MonoBehaviour
{
    [SerializeField] GameObject dummyProjectile;

    BattleWorld m_battleWorld;

    private void Awake()
    {
        m_battleWorld = GetComponentInParent<BattleWorld>();
        Debug.Assert(dummyProjectile != null, "No tenemos dummy Projectile");
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
            List<Effect> onImpactEffects = new List<Effect>();
            onImpactEffects.Add(new EffectDamage());
            ShapeProjectileForward bouncedProjectile = new ShapeProjectileForward(dummyProjectile, onImpactEffects);
            onImpactEffects.Add(new EffectSpawn(bouncedProjectile));

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
