using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWindow : MonoBehaviour
{
    [SerializeField] PlayerInfoWidget m_playerInfo;

    public void Awake()
    {
        Debug.Assert(m_playerInfo != null, "No tenemos player Info");
    }


    public void Initialize(GameObject i_character)
    {
        m_playerInfo.Configure(i_character);
    }
}
