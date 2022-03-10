using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] World m_explorationWorld;
    [SerializeField] BattleWorld m_battleWorld;
    [SerializeField] UIDirector m_ui;
    [SerializeField] BiomeDirector m_biomeDirector;
    [SerializeField] Cheats m_cheats;
    enum PauseState
    {
        Active,
        Paused
    }

    enum GameState
    {
        Exploration,
        Battle
    }

    GameState m_gameState = GameState.Exploration;
    PauseState m_pauseState = PauseState.Active;
    public void Awake()
    {
        Debug.Assert(m_explorationWorld != null, "World is not assigned in GameDirector");
        Debug.Assert(m_ui != null, "UI is not assigned in GameDirector");
        Debug.Assert(m_biomeDirector != null, "No tenemos biome director");
        m_cheats.Initialize();
    }


    public void UserRequestedPause()
    {
       switch (m_pauseState)
        {
            case PauseState.Active:
                //Parar el mundo
                switch(m_gameState)
                {
                    case GameState.Exploration:
                        m_explorationWorld.gameObject.SetActive(false);
                        break;
                    case GameState.Battle:
                        m_battleWorld.gameObject.SetActive(false);
                        break;
                }
                //Activar el blurr
                m_ui.ActivatePause();
                m_pauseState = PauseState.Paused;
                break;
            case PauseState.Paused:
                //Activar el mundo
                switch (m_gameState)
                {
                    case GameState.Exploration:
                        m_explorationWorld.gameObject.SetActive(true);
                        break;
                    case GameState.Battle:
                        m_battleWorld.gameObject.SetActive(true);
                        break;
                }
                //desactivar el blurr
                m_ui.DeactivatePause();
                m_pauseState = PauseState.Active;
                break;
        }
    }

    void Start()
    {
        Biome currentBiome = m_biomeDirector.PrepareBiome();
        StartExploration(currentBiome);
        //EnterBattle(new BattleDef(), null);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_gameState == GameState.Battle)
        {
            BattleWorld.BattleState battleState = m_battleWorld.GetBattleState();
            if(battleState.finished)
            {
                GoBackToExploration();
            }
        }
    }

   public void RequestBattleStart(BattleDef i_battleDef)
   {
        Debug.Assert(m_pauseState == PauseState.Active);
        if (m_gameState == GameState.Battle)
        {
            return;
        }

        EnterBattle(i_battleDef, null, m_biomeDirector.GetCurrentBiome());
    }

    void EnterBattle(BattleDef battleDef, Spell i_spellToTest, Biome i_currentBiome)
    {
        //TODO: Que sea con los enemigos que tocan
        m_explorationWorld.gameObject.SetActive(false);
        m_battleWorld.Initialize(i_spellToTest, i_currentBiome);
        m_battleWorld.gameObject.SetActive(true);
        m_gameState = GameState.Battle;
        m_ui.TransitionToBattle(m_battleWorld.player.gameObject);
    }

    public void RequestExplorationMode()
    {
        Debug.Assert(m_pauseState == PauseState.Active);
        if (m_gameState == GameState.Exploration)
        {
            return;
        }

        StartExploration(m_biomeDirector.GetCurrentBiome());
        
    }

    public MenuFuture<uint> RequestTrainingBattle(Spell i_spellToTest)
    {
        MenuResult<uint> result = new MenuResult<uint>(0);
        EnterBattle(new BattleDef(), i_spellToTest, m_biomeDirector.GetCurrentBiome());
        return result;
    }

    void StartExploration(Biome i_biome)
    {
        m_explorationWorld.Initialize(i_biome);
        m_battleWorld.gameObject.SetActive(false);
        m_explorationWorld.gameObject.SetActive(true);
        m_gameState = GameState.Exploration;
        m_ui.TransitionToExploration();
    }

    void GoBackToExploration()
    {
        m_battleWorld.gameObject.SetActive(false);
        m_explorationWorld.gameObject.SetActive(true);
        m_gameState = GameState.Exploration;
        m_ui.TransitionToExploration();
    }

    
}
