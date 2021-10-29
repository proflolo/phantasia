using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParser
{
   public enum ValidationMode
    {
        Partial,
        Full
    }

   public static Spell ParseSpell(IList<RuneDef> i_runes, ValidationMode i_validationMode)
   {
        //magia de la forja
        //Contaremos los costes (forja y lanzamiento)
        if (i_runes.Count == 0)
        {
            return null;
        }

        ParseResult<Shape> shapeToSpawn = ParseShape(i_runes, 0, i_validationMode);
        //Error de parseo!
        if (shapeToSpawn.element == null)
        {
            return null;
        }
        else if (shapeToSpawn.readSize != i_runes.Count)
        {
            return null;
        }

        EffectSpawn initialEffect = new EffectSpawn(shapeToSpawn.element);
        List < Effect > effects = new List<Effect>();
        effects.Add(initialEffect);
        return new Spell(effects);
    }

    struct ParseResult<T>
    {
        public T element;
        public int readSize;
    }
    private static ParseResult<IList<Effect>> ParseEffects(IList<RuneDef> i_runes, int i_index, ValidationMode i_validationMode)
    {
        ParseResult<IList<Effect>> result;
        result.element = null;
        result.readSize = 0;

        if (i_runes.Count <= i_index)
        {
            return result;
        }


        //if (rune.type != RuneDef.Type.Effect && rune.type != RuneDef.Type.Special)
        //{
        //    return result;
        //}

        List<Effect> effectList = new List<Effect>();
        bool finished = false;
        bool spawnShapePending = false;
        while (!finished)
        {
            if(spawnShapePending == true) 
            {
                result.element = null;
                return result;
            }
            RuneDef rune = i_runes[i_index + result.readSize];
            result.readSize++;
            switch (rune)
            {
                case RuneEffectDamageDef specific:
                    effectList.Add(new EffectDamage(specific.damage));

                    break;
                case RuneEffectSpawnDef specific:
                    ParseResult<Shape> shapeToSpawn = ParseShape(i_runes, i_index + result.readSize, i_validationMode);
                    if(shapeToSpawn.element == null && i_validationMode == ValidationMode.Full)
                    {
                        return result;
                    }
                    else if (shapeToSpawn.element == null && i_validationMode == ValidationMode.Partial)
                    {
                        spawnShapePending = true;
                    }
                    result.readSize += shapeToSpawn.readSize;
                    effectList.Add(new EffectSpawn(shapeToSpawn.element));
                    break;
                case RuneSeparatorDef specific:
                    if(result.readSize == 1)
                    {
                        return result;
                    }
                    finished = true;
                    break;
                default:
                    return result;
            }
            if (i_runes.Count <= i_index + result.readSize)
            {
                finished = true;
            }
        }

        result.element = effectList;

        return result;
    }

    private static ParseResult<Shape> ParseShape(IList<RuneDef> i_runes, int i_index, ValidationMode i_validationMode)
    {
        ParseResult<Shape> result;
        result.element = null;
        result.readSize = 0;

        if (i_runes.Count <= i_index)
        {
            return result;
        }
        RuneDef rune = i_runes[i_index];
        if (rune.type != RuneDef.Type.Shape)
        {
            return result;
        }

        switch (rune)
        {
            case RuneShapeExplosionDef specific:
                ParseResult<IList<Effect>> explosioneffects = ParseEffects(i_runes, i_index + 1, i_validationMode);
                if (explosioneffects.element == null)
                {
                    explosioneffects.element = new List<Effect>();
                    explosioneffects.element.Add(new EffectDamage(1)); //TODO
                    explosioneffects.readSize = 0;
                }
                result.element = new ShapeExplosion(specific.explosionTemplate, explosioneffects.element, 1.0f, specific.radius);
                result.readSize = explosioneffects.readSize + 1;
                return result;

            case RuneShapeProjectileDef specific:
                ParseResult<IList<Effect>> projectileEffects = ParseEffects(i_runes, i_index + 1, i_validationMode);
                if (projectileEffects.element == null)
                {
                    projectileEffects.element = new List<Effect>();
                    projectileEffects.element.Add(new EffectDamage(1)); //TODO
                    projectileEffects.readSize = 0;
                }
                result.element = new ShapeProjectileForward(specific.projectileShape, projectileEffects.element);
                result.readSize = projectileEffects.readSize + 1;
                return result;
            default:
                Debug.Assert(false, "Tipo de shape desconocido");
                return result;
        }
    }
}
