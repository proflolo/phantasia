using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvetory
{
    public delegate void ItemAmountChanged(ItemDef i_item, uint i_oldamount, uint i_newAmount);
    uint GetItemAmount(ItemDef i_item);
    ItemAmountChanged sig_itemAmountChanged { get; set; }
}

public class Inventory: IInvetory
{
    public Inventory()
    {
    }

   

    public void AddItem(ItemDef i_item, uint i_itemAmount)
    {
        //m_data.stock.Add(i_item)
        if(m_data.stock.ContainsKey(i_item))
        {
            uint oldAmount = m_data.stock[i_item];
            m_data.stock[i_item] += i_itemAmount;
            uint newamount = oldAmount + i_itemAmount;
            m_sig_itemAmountChanged(i_item, oldAmount, newamount);
        }
        else
        {
            m_data.stock.Add(i_item, i_itemAmount);
            m_sig_itemAmountChanged(i_item, 0, i_itemAmount);
        }
    }

    public class SaveData
    {
        public Dictionary<ItemDef, uint> stock = new Dictionary<ItemDef, uint>();
    }

    public uint GetItemAmount(ItemDef i_item)
    {
        if (m_data.stock.ContainsKey(i_item))
        {
            return m_data.stock[i_item];
        }
        else
        {
            return 0;
        }
    }

    public SaveData m_data;
    private IInvetory.ItemAmountChanged m_sig_itemAmountChanged;
    public IInvetory.ItemAmountChanged sig_itemAmountChanged
    { 
        get
        {
            return m_sig_itemAmountChanged;
        }

        set
        {
            m_sig_itemAmountChanged = value;
        }
      }
}
