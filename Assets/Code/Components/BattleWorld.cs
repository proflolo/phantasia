using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWorld : MonoBehaviour
{
    Spell m_trainingSpell;
    [SerializeField] BattleMCController m_player;
    Biome m_currentBiome;
    [SerializeField] ScenarioDirector m_scenarioDirector;


    public struct BattleState
    {
        BattleState(bool i_finished = false)
        {
            finished = i_finished;
        }
        public bool finished;
    };

    BattleState m_state;

    private void Awake()
    {
        Debug.Assert(m_scenarioDirector != null, "No tienes scenario director");
        Debug.Assert(m_player != null, "No hay jugador!");
        m_battleAInfo = new AIBase.BattleAIInfo();
        m_battleAInfo.player = m_player.gameObject;
    }
    public void Initialize(Spell i_trainingSpell, Biome i_currentBiome)
    {
        m_trainingSpell = i_trainingSpell;
        m_currentBiome = i_currentBiome;
        m_scenarioDirector.GenerateMap(i_currentBiome);
        m_state = new BattleState();
        m_debugEllapsedTime = 0.0f;

    }

    float m_debugEllapsedTime = 0.0f;
    private void Update()
    {
        m_debugEllapsedTime += Time.deltaTime;
        if(m_debugEllapsedTime > 2.0f)
        {
            m_state.finished = true;
        }
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

    public BattleState GetBattleState()
    {
        return m_state;
    }
}
