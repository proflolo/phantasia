using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEditorWindow : MonoBehaviour
{
    Spell m_spell;
    [SerializeField] RectTransform m_runesContainer;
    RuneEntry m_entryTemplate;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Assert(m_runesContainer != null, "Runes Container es nulo");
        m_entryTemplate = m_runesContainer.GetChild(0).GetComponent<RuneEntry>();
        //m_entryTemplate.transform.parent = null;
        m_entryTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        foreach(RuneDef rune in Game.instance.database.ListAllRunes())
        {
            GameObject newEntry = Instantiate(m_entryTemplate.gameObject, m_runesContainer.transform);
            RuneEntry runeEntry = newEntry.GetComponent<RuneEntry>();
            runeEntry.Configure(rune);
            newEntry.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
