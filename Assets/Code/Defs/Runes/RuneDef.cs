using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

//[CreateAssetMenu(fileName = "Item Definition", menuName = "Game/Item Definition", order = 2)]
public class RuneDef : ScriptableObject
{
    [SerializeField] char m_runeLetter;
    [SerializeField] LocalizedString m_name;
    [SerializeField] uint m_forgeCost;
    [SerializeField] uint m_castCostBase;
    public enum Type
    {
        Shape,
        Effect,
        Special
    }
    [SerializeField] Type m_type;

    public string letter
    {
        get
        {
            return m_runeLetter.ToString();
        }
    }

    public LocalizedString description
    {
        get
        {
            return m_name;
        }
    }

    public Type type
    {
        get
        {
            return m_type;
        }
    }

    public uint forgeCost
    {
        get
        {
            return m_forgeCost;
        }
    }

    public uint castCostBase
    {
        get
        {
            return m_castCostBase;
        }
    }
}
