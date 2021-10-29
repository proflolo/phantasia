using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "Game/Rune/Shape/Explosion", order = 1)]
public class RuneShapeExplosionDef : RuneDef
{
    [SerializeField] GameObject m_explosionTemplate;
    [SerializeField] float m_radius;

    public GameObject explosionTemplate
    {
        get
        {
            return m_explosionTemplate;
        }
    }

    public float radius
    {
        get
        {
            return m_radius;
        }
    }
}
