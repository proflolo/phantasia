using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cheats Config", menuName = "Game/Cheats", order = 2)]
public class Cheats : ScriptableObject
{
    [SerializeField] bool m_generateEnemies = true;

    static Cheats s_instance = null;
    public void Initialize()
    {
        Debug.Assert(s_instance == null);
        s_instance = this;
    }

    public static Cheats instance
    {
        get
        {
            return s_instance;
        }
    }

    public bool generateEnemies
    {
        get
        {
            #if UNITY_EDITOR
                return m_generateEnemies;
            #else
                return true;
            #endif
        }
    }
}
