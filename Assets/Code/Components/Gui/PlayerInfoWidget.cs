using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoWidget : MonoBehaviour
{
    [SerializeField] Text m_currentLife;
    [SerializeField] Text m_maxLife;

    private void Awake()
    {
        
    }

    public void Configure(GameObject i_player)
    {
        LifeComponent lifeComponent = i_player.GetComponent<LifeComponent>();
        if(lifeComponent)
        {
            if(m_maxLife)
            {
                m_maxLife.text = lifeComponent.maxHP.ToString();
            }
            if(m_currentLife)
            {
                m_currentLife.text = lifeComponent.currentHP.ToString();
                lifeComponent.sig_onInteracted.AddListener((uint i_old, uint i_new) => OnLifeChanged(i_old, i_new));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLifeChanged(uint i_old, uint i_new)
    {
        if (m_currentLife)
        {
            m_currentLife.text = i_new.ToString();
        }
    }
}
