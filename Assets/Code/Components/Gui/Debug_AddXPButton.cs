using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class Debug_AddXPButton : MonoBehaviour
{
    Button m_buttonComponent;
    private void Awake()
    {
        m_buttonComponent = GetComponent<Button>();    
    }

    public void OnAction()
    {
        //Dar XP
    }
}
