using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabase
{
    ItemDef FindItemByName(string i_name);
    List<ItemDef> ListAllManaItems();
}

public class Database: IDatabase
{
    public Database()
    {
        m_itemsByName = new Dictionary<string, ItemDef>();
        //Carga
        ItemDef[] allItems = Resources.LoadAll<ItemDef>("Database/Items/");
        foreach(ItemDef item in allItems)
        {
            m_itemsByName.Add(item.name, item);
        }
    }

    public ItemDef FindItemByName(string i_name)
    {
        if(m_itemsByName.ContainsKey(i_name))
        {
            return m_itemsByName[i_name];
        }
        else
        {
            return null;
        }
    }

    public List<ItemDef> ListAllManaItems()
    {
        List<ItemDef> allMana = new List<ItemDef>();
        foreach(ItemDef item in m_itemsByName.Values)
        {
            allMana.Add(item);
        }
        return allMana;
    }

    Dictionary<string, ItemDef> m_itemsByName;
}
