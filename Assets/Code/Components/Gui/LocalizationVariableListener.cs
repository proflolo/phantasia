using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(LocalizeStringEvent))]
public class LocalizationVariableListener : MonoBehaviour
{
    public LocalizationVariable[] m_variablesToListen;
    LocalizeStringEvent m_localizedLabel;
    private void Awake()
    {
        m_localizedLabel = GetComponent<LocalizeStringEvent>();
    }
    private void Start()
    {
        foreach(LocalizationVariable locVariable in m_variablesToListen)
        {
            locVariable.sig_onChanged.AddListener(() => OnVariableChanged());
        }
    }

    private void OnVariableChanged()
    {
        m_localizedLabel.RefreshString();
    }
}
