using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BattleDeco2D : MonoBehaviour
{
    SpriteRenderer m_renderer;
    private void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        ScenarioDirector director = GetComponentInParent<ScenarioDirector>();
        director.AddBattleDeco(this);
    }
    public enum DecoType
    {
        Big
        , Small
        , Far
    }

    [SerializeField] DecoType m_desiredDeco;

    public DecoType decoType
    {
        get
        {
            return m_desiredDeco;
        }
    }

    public void SetSprite(Sprite i_sprite)
    {
        if(i_sprite != null)
        {
            m_renderer.sprite = i_sprite;
        }
        else
        {
            m_renderer.enabled = false;
        }
    }
}
