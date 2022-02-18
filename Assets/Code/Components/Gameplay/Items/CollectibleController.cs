using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Reward))]
[RequireComponent(typeof(SpriteRenderer))]
public class CollectibleController : MonoBehaviour
{
    Reward m_reward;
    SpriteRenderer m_renderer;
    // Start is called before the first frame update
    void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_reward = GetComponent<Reward>();
    }

    public void Config(ItemDef i_item)
    {
        m_renderer.sprite = i_item.icon;
        m_reward.Config(i_item, 1);
    }
}
