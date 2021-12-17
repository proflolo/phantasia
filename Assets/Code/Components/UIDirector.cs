using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(Animator))]
public class UIDirector : MonoBehaviour
{
    [SerializeField] GameObject m_blurWidget;
    [SerializeField] GameObject m_pauseMenu;
    [SerializeField] GameObject m_explorationMenu;
    [SerializeField] BattleWindow m_battleMenu;
    [SerializeField] SpellEditorWindow m_spellForgeMenu;
    [SerializeField] GameDirector m_director;

    Animator m_animator;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Assert(m_director != null, "No tenemos Game Director");
        Debug.Assert(m_blurWidget != null, "Blur widget not set in UI Director");
        Debug.Assert(m_pauseMenu != null, "Pause menu not set in UI Director");
        Debug.Assert(m_explorationMenu != null, "Exploration menu not set in UI Director");
        Debug.Assert(m_battleMenu != null, "Battle menu not set in UI Director");
        Debug.Assert(m_spellForgeMenu != null, "Spell Forge menu not set in UI Director");
        m_animator = GetComponent<Animator>();
        
    }


    void Start()
    {
        m_blurWidget.gameObject.SetActive(false);
        m_pauseMenu.gameObject.SetActive(false);
        m_spellForgeMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePause()
    {
        m_blurWidget.gameObject.SetActive(true);
        //m_pauseMenu.gameObject.SetActive(true);
        //m_spellForgeMenu.gameObject.SetActive(true);
        IEnumerator menuExecution = ExecuteSpellForgeMenu();
        StartCoroutine(menuExecution);
        //await ExecuteSpellForgeMenu2(m_spellForgeMenu);

    }
    public void DeactivatePause()
    {
        m_blurWidget.gameObject.SetActive(false);
        //m_pauseMenu.gameObject.SetActive(false);
        //m_spellForgeMenu.gameObject.SetActive(false);
    }

    //public async Task ExecuteSpellForgeMenu2(GameObject i_spellForgeMenu)
    //{
    //    i_spellForgeMenu.SetActive(true);
    //    //Esperate a que el Menu se cierre
    //    //yield return new WaitForSeconds(3);
    //    //i_spellForgeMenu.SetActive(false);
    //    //Pedir que se despause
    //    //m_director.UserRequestedPause();
    //}
    

    IEnumerator ExecuteSpellForgeMenu()
    {
        m_spellForgeMenu.gameObject.SetActive(true);
        //Esperate a que el Menu se cierre
        MenuFuture<Spell> result = m_spellForgeMenu.Execute();
        yield return result;
        Spell spell = result.GetResult();
        m_spellForgeMenu.gameObject.SetActive(false);
        //Pedir que se despause
        m_director.UserRequestedPause();
        //Batalla time!
        MenuFuture<uint> trainingBattle = m_director.RequestTrainingBattle(spell);
        yield return trainingBattle;
    }
        


   
    public void TransitionToBattle(GameObject i_character)
    {
        m_battleMenu.Initialize(i_character);
        m_animator.SetBool("in_battle", true);
    }


    public void TransitionToExploration()
    {
        m_animator.SetBool("in_battle", false);
    }
}
