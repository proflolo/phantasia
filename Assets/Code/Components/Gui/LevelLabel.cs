using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelLabel : MonoBehaviour
{
    Text m_textComponent;

    private void Awake()
    {
        m_textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_textComponent.text = Game.instance.character.GetLevel().ToString(); 
    }
}
