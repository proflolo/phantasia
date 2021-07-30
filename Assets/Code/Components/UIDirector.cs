using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIDirector : MonoBehaviour
{
    [SerializeField] GameObject m_blurWidget;
    [SerializeField] GameObject m_pauseMenu;
    [SerializeField] GameObject m_explorationMenu;
    [SerializeField] GameObject m_battleMenu;


    Animator m_animator;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Assert(m_blurWidget != null, "Blur widget not set in UI Director");
        Debug.Assert(m_pauseMenu != null, "Pause menu not set in UI Director");
        Debug.Assert(m_explorationMenu != null, "Exploration menu not set in UI Director");
        Debug.Assert(m_battleMenu != null, "Battle menu not set in UI Director");
        m_animator = GetComponent<Animator>();
    }


    void Start()
    {
        m_blurWidget.gameObject.SetActive(false);
        m_pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePause()
    {
        m_blurWidget.gameObject.SetActive(true);
        m_pauseMenu.gameObject.SetActive(true);

    }
    public void DeactivatePause()
    {
        m_blurWidget.gameObject.SetActive(false);
        m_pauseMenu.gameObject.SetActive(false);

    }


   
    public void TransitionToBattle()
    {
        m_animator.SetBool("in_battle", true);
    }


    public void TransitionToExploration()
    {
        m_animator.SetBool("in_battle", false);
    }
}
