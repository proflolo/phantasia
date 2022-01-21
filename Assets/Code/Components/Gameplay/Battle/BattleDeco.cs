using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDeco : MonoBehaviour
{
    
    private void Start()
    {
        ScenarioDirector director = GetComponentInParent<ScenarioDirector>();
        director.AddBattleDeco(this);
    }
    public enum DecoType
    {
        Tall
        , Short
    }

    [SerializeField] DecoType m_desiredDeco;

    public DecoType decoType
    {
        get
        {
            return m_desiredDeco;
        }
    }
}
