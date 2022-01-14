using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(SpellUI))]
public class SpellEditorWindow : MonoBehaviour
{
    List<RuneDef> m_selectedRunes;
    [SerializeField] RectTransform m_runesContainer;
    [SerializeField] RectTransform m_spellContainer;
    //[SerializeField] Text m_spellCastCostText;
    //[SerializeField] Text m_spellForgeCostText;
    SpellUI m_spellUI;
    [SerializeField] Button m_forgeButton;
    [SerializeField] Button m_tryButton;
    

    RuneEntry m_entryTemplate;
    RuneDisplay m_displayTemplate;
    MenuResult<Spell> m_result;
    List<RuneEntry> m_runeKeys;

    // Start is called before the first frame update
    private void Awake()
    {
        m_spellUI = GetComponent<SpellUI>();
        Debug.Assert(m_runesContainer != null, "Runes Container es nulo");
        Debug.Assert(m_spellContainer != null, "Spell Container es nulo");
        Debug.Assert(m_spellUI != null, "No tenemos Spell UI");
        Debug.Assert(m_forgeButton != null, "Forge Button es nulo");
        Debug.Assert(m_tryButton != null, "Try Button es nulo");
        m_entryTemplate = m_runesContainer.GetChild(0).GetComponent<RuneEntry>();
        m_displayTemplate = m_spellContainer.GetChild(0).GetComponent<RuneDisplay>();
        //m_entryTemplate.transform.parent = null;
        m_entryTemplate.gameObject.SetActive(false);
        m_displayTemplate.gameObject.SetActive(false);

    }

    private void UpdateCostTexts()
    {
        Spell spell = SpellParser.ParseSpell(m_selectedRunes, SpellParser.ValidationMode.Partial);
        if(spell != null)
        {
            m_spellUI.spell = spell;
        }
    }

    private void UpdateButtonStates()
    {
        Spell result = SpellParser.ParseSpell(m_selectedRunes, SpellParser.ValidationMode.Full);
        if(m_selectedRunes.Count == 0 || result == null)
        {
            m_forgeButton.interactable = false;
            m_tryButton.interactable = false;
        }
        else
        {
            m_forgeButton.interactable = true;
            m_tryButton.interactable = true;
        }
    }

    public MenuFuture<Spell> Execute()
    {
        m_selectedRunes = new List<RuneDef>();
        m_runeKeys = new List<RuneEntry>();
        m_result = new MenuResult<Spell>(null);
        UpdateCostTexts();
        UpdateButtonStates();
        UpdateRunesInSpell();
        UpdateRuneButtons();
        return m_result;
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
            m_runeKeys.Clear();
            foreach (RuneDef rune in Game.instance.database.ListAllRunes())
            {
                GameObject newEntry = Instantiate(m_entryTemplate.gameObject, m_runesContainer.transform);
                newEntry.SetActive(true);
                RuneEntry runeEntry = newEntry.GetComponent<RuneEntry>();
                runeEntry.Configure(rune);
                Button runeButton = runeEntry.GetComponent<Button>();
                runeButton.onClick.AddListener(() => RuneSelected(rune));
                m_runeKeys.Add(runeEntry);
            }
            m_dirty = false;
            StartCoroutine(UpdateLayouts());
            UpdateRuneButtons();
        }

    }

    void UpdateRunesInSpell()
    {
        int i = 0;
        while (i < m_selectedRunes.Count && i+1 < m_spellContainer.childCount)
        {
            RuneDisplay runeVisual = m_spellContainer.GetChild(i+1).GetComponent<RuneDisplay>();
            RuneDef rune = m_selectedRunes[i];
            if(rune == runeVisual.GetRune())
            {
                //All good
            }
            else
            {
                break;
            }

            i++;
        }

        if (i+1 < m_spellContainer.childCount)
        {
            for(int j = i+1; j < m_spellContainer.childCount; j++)
            {
                Destroy(m_spellContainer.GetChild(j).gameObject);
            }
        }

        if (i < m_selectedRunes.Count)
        {
            for (int j = i; j < m_selectedRunes.Count; j++)
            {
                GameObject newRune = Instantiate(m_displayTemplate.gameObject, m_spellContainer);
                newRune.SetActive(true);
                RuneDisplay runeDisplay = newRune.GetComponent<RuneDisplay>();
                runeDisplay.Configure(m_selectedRunes[j]);
            }
        }
    }

    void RuneSelected(RuneDef i_rune)
    {
        //Debug.Log("Selected a Rune " + i_rune.description);
        //Puedo selecionar esta runa?
        bool valid = true; //TODO
        if (valid)
        {
            m_selectedRunes.Add(i_rune);
            UpdateRunesInSpell();
            UpdateCostTexts();
            UpdateButtonStates();
            UpdateRuneButtons();
        }

        StartCoroutine(UpdateLayouts());
    }

    void UpdateRuneButtons()
    {
        foreach(RuneEntry entry in m_runeKeys)
        {
            Button button = entry.GetComponent<Button>();
            bool canBeAdded = false; //TODO
            m_selectedRunes.Add(entry.GetRune());
            Spell s = SpellParser.ParseSpell(m_selectedRunes, SpellParser.ValidationMode.Partial);
            if(s != null)
            {
                canBeAdded = true;
            }
            button.interactable = canBeAdded;
            m_selectedRunes.RemoveAt(m_selectedRunes.Count - 1);
        }
    }

    public void OnBack()
    {
        if(m_selectedRunes.Count == 0)
        {
            m_result.SetReady(null);
        }
        else
        {
            m_selectedRunes.RemoveAt(m_selectedRunes.Count - 1);
            UpdateCostTexts();
            UpdateRunesInSpell();
            UpdateButtonStates();
            UpdateRuneButtons();
        }
    }

    IEnumerator UpdateLayouts()
    {
        yield return new WaitForEndOfFrame();
        m_runesContainer.GetComponent<LayoutGroup>().enabled = false;
        m_runesContainer.GetComponent<LayoutGroup>().CalculateLayoutInputHorizontal();
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_runesContainer);
        m_runesContainer.GetComponent<LayoutGroup>().enabled = true;

        m_spellContainer.GetComponent<LayoutGroup>().enabled = false;
        m_spellContainer.GetComponent<LayoutGroup>().CalculateLayoutInputHorizontal();
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_spellContainer);
        m_spellContainer.GetComponent<LayoutGroup>().enabled = true;
    }

    public void FinishPressed()
    {
        Debug.Assert(m_result != null);
        m_result.SetReady(SpellParser.ParseSpell(m_selectedRunes, SpellParser.ValidationMode.Full));
    }

    public void OnTryPressed()
    {
        Spell result = SpellParser.ParseSpell(m_selectedRunes, SpellParser.ValidationMode.Full);
        m_result.SetReady(result);
    }
}
