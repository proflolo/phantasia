using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class InventoryEntry : MonoBehaviour
{
    [SerializeField] Image m_icon;
    [SerializeField] Text m_amount;
    Animator m_animator;
    ItemDef m_item;
    uint m_lastItemAmount = 0;
    float m_remainingDisplaySeconds = 3.0f;
    

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        Debug.Assert(m_icon, "No hay icono en la entrada de inventario");
        Debug.Assert(m_amount, "No hay label en la entrada de inventario");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(m_item)
        {
            m_lastItemAmount = Game.instance.inventory.GetItemAmount(m_item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_remainingDisplaySeconds >= 0)
        {
            m_remainingDisplaySeconds -= Time.deltaTime;
            if (m_remainingDisplaySeconds <= 0.0f)
            {
                m_animator.SetTrigger("hide");
            }
        }
    }

    public bool CanAmountBeUpdated()
    {
        return m_remainingDisplaySeconds > 0.0f;
    }

    public ItemDef GetItem()
    {
        return m_item;
    }

    public void UpdateAmount()
    {
        m_remainingDisplaySeconds = 3.0f;
        m_amount.text = Game.instance.inventory.GetItemAmount(m_item).ToString();
        //m_animator.SetTrigger("updated");
    }

    public void DisplayItem(ItemDef i_item)
    {
        m_item = i_item;
        m_icon.sprite = i_item.icon;
        m_amount.text = Game.instance.inventory.GetItemAmount(i_item).ToString();
        m_animator.SetTrigger("updated");
    }
}
