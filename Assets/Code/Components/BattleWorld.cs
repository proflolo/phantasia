using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWorld : MonoBehaviour
{
    Spell m_trainingSpell;
    [SerializeField] BattleMCController m_player;

    private void Awake()
    {
        Debug.Assert(m_player != null, "No hay jugador!");
        m_battleAInfo = new AIBase.BattleAIInfo();
        m_battleAInfo.player = m_player.gameObject;
    }
    public void Initialize(Spell i_trainingSpell)
    {
        m_trainingSpell = i_trainingSpell;
    }

    public Spell GetTrainingSpell()
    {
        return m_trainingSpell;
    }

    public AIBase.BattleAIInfo GetBattleAIInfo()
    {
        return m_battleAInfo;
    }

    AIBase.BattleAIInfo m_battleAInfo;

    public BattleMCController player
    {
        get
        {
            return m_player;
        }
    }
}
