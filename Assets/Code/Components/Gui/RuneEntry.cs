using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RuneDisplay))]
[RequireComponent(typeof(Button))]
public class RuneEntry : MonoBehaviour
{
    RuneDisplay m_display;

    private void Awake()
    {
        m_display = GetComponent<RuneDisplay>();
    }

    public void Configure(RuneDef i_rune)
    {
        m_display.Configure(i_rune);
    }
}
