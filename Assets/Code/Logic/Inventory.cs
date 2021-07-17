using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Inventory()
    {
    }
    public void AddItem(ItemDef i_item, uint i_itemAmount)
    {
        //m_data.stock.Add(i_item)
        if(m_data.stock.ContainsKey(i_item))
        {
            m_data.stock[i_item] += i_itemAmount;
        }
        else
        {
            m_data.stock.Add(i_item, i_itemAmount);
        }
    }

    public class SaveData
    {
        public Dictionary<ItemDef, uint> stock = new Dictionary<ItemDef, uint>();
    }

    public SaveData m_data;

}
