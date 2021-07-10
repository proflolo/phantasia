using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_world;
    [SerializeField] UIDirector m_ui;
    enum PauseState
    {
        Active,
        Paused
    }

    PauseState m_pauseState = PauseState.Active;
    public void Awake()
    {
        Debug.Assert(m_world != null, "World is not assigned in GameDirector");
        Debug.Assert(m_ui != null, "UI is not assigned in GameDirector");
    }


    public void UserRequestedPause()
    {
        int level = Game.instance.character.GetLevel();

       switch (m_pauseState)
        {
            case PauseState.Active:
                //Parar el mundo
                m_world.SetActive(false);
                //Activar el blurr
                m_ui.ActivatePause();
                m_pauseState = PauseState.Paused;
                break;
            case PauseState.Paused:
                //Activar el mundo
                m_world.SetActive(true);
                //desactivar el blurr
                m_ui.DeactivatePause();
                m_pauseState = PauseState.Active;
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
