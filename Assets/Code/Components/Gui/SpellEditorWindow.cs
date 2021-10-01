using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellEditorWindow : MonoBehaviour
{
    Spell m_spell;
    [SerializeField] RectTransform m_runesContainer;
    [SerializeField] RectTransform m_spellContainer;
    RuneEntry m_entryTemplate;
    RuneDisplay m_displayTemplate;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Assert(m_runesContainer != null, "Runes Container es nulo");
        Debug.Assert(m_spellContainer != null, "Spell Container es nulo");
        m_entryTemplate = m_runesContainer.GetChild(0).GetComponent<RuneEntry>();
        m_displayTemplate = m_spellContainer.GetChild(0).GetComponent<RuneDisplay>();
        //m_entryTemplate.transform.parent = null;
        m_entryTemplate.gameObject.SetActive(false);
        m_displayTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        

       

        
    }

    bool m_dirty = true;
    // Update is called once per frame
    void Update()
    {
        if(m_dirty)
        {
            foreach (RuneDef rune in Game.instance.database.ListAllRunes())
            {
                GameObject newEntry = Instantiate(m_entryTemplate.gameObject, m_runesContainer.transform);
                newEntry.SetActive(true);
                RuneEntry runeEntry = newEntry.GetComponent<RuneEntry>();
                runeEntry.Configure(rune);
                Button runeButton = runeEntry.GetComponent<Button>();
                runeButton.onClick.AddListener(() => RuneSelected(rune));
            }
            m_dirty = false;
            //m_runesContainer.ForceUpdateRectTransforms();
            //m_runesContainer.GetComponent<HorizontalLayoutGroup>().spacing = 5.001f;
            //LayoutRebuilder.MarkLayoutForRebuild(m_runesContainer);
        }
    }

    void RuneSelected(RuneDef i_rune)
    {
        //Debug.Log("Selected a Rune " + i_rune.description);
        //Puedo selecionar esta runa?
        bool valid = true; //TODO
        if (valid)
        {
            GameObject newRune = Instantiate(m_displayTemplate.gameObject, m_spellContainer);
            newRune.SetActive(true);
            RuneDisplay runeDisplay = newRune.GetComponent<RuneDisplay>();
            runeDisplay.Configure(i_rune);
        }
    }
}
