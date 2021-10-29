using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Game/Rune/Shape/Projectile", order = 2)]
public class RuneShapeProjectileDef : RuneDef
{
    [SerializeField] GameObject m_projectileShape;

    public GameObject projectileShape
    {
        get
        {
            return m_projectileShape;
        }
    }
}
