using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationVariable : MonoBehaviour
{
    [System.Serializable]
    public class OnChanged : UnityEvent { }

    public OnChanged sig_onChanged;

    private void Awake()
    {
        if(sig_onChanged == null)
        {
            sig_onChanged = new OnChanged();
        }
    }
}


