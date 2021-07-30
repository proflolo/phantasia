using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_explorationWorld;
    [SerializeField] GameObject m_battleWorld;
    [SerializeField] UIDirector m_ui;
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
    }


    public void UserRequestedPause()
    {
        int level = Game.instance.character.GetLevel();

       switch (m_pauseState)
        {
            case PauseState.Active:
                //Parar el mundo
                switch(m_gameState)
                {
                    case GameState.Exploration:
                        m_explorationWorld.SetActive(false);
                        break;
                    case GameState.Battle:
                        m_battleWorld.SetActive(false);
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
                        m_explorationWorld.SetActive(true);
                        break;
                    case GameState.Battle:
                        m_battleWorld.SetActive(true);
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
        EnterExploration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void RequestBattleStart(BattleDef i_battleDef)
   {
        Debug.Assert(m_pauseState == PauseState.Active);
        if (m_gameState == GameState.Battle)
        {
            return;
        }

        EnterBattle(i_battleDef);
    }

    void EnterBattle(BattleDef battleDef)
    {
        //TODO: Que sea con los enemigos que tocan
        m_explorationWorld.SetActive(false);
        m_battleWorld.SetActive(true);
        m_gameState = GameState.Battle;
        m_ui.TransitionToBattle();
    }

    public void RequestExplorationMode()
    {
        Debug.Assert(m_pauseState == PauseState.Active);
        if (m_gameState == GameState.Exploration)
        {
            return;
        }

        EnterExploration();
        
    }

    void EnterExploration()
    {
        m_explorationWorld.SetActive(true);
        m_battleWorld.SetActive(false);
        m_gameState = GameState.Exploration;
        m_ui.TransitionToExploration();
    }

    
}
