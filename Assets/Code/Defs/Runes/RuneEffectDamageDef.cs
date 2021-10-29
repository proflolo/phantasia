using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Game/Rune/Effect/Damage", order = 1)]
public class RuneEffectDamageDef : RuneDef
{
    [SerializeField] uint m_damage;

    public uint damage
    {
        get
        {
            return m_damage;
        }
    }

}
