using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneDisplay : MonoBehaviour
{
    [SerializeField] Text m_runeLetter;
    [SerializeField] Text m_runeDescription;

    private void Awake()
    {
        Debug.Assert(m_runeDescription != null, "No hay runeDescription");
        Debug.Assert(m_runeLetter != null, "No hay runeLetter");
    }

    public void Configure(RuneDef i_rune)
    {
        m_runeLetter.text = i_rune.letter;
        m_runeDescription.text = i_rune.description;
    }
}
