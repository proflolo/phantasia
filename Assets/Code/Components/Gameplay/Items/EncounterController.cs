using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnterBattle))]
[RequireComponent(typeof(SpriteRenderer))]
public class EncounterController : MonoBehaviour
{
    EnterBattle m_battle;
    SpriteRenderer m_renderer;
    // Start is called before the first frame update
    void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_battle = GetComponent<EnterBattle>();
    }

    public void Config(BattleDef i_battle)
    {
        m_renderer.sprite = i_battle.enemies[0].icon;
        m_battle.Config(i_battle);
    }
}
