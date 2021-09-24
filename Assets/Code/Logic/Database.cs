using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabase
{
    ItemDef FindItemByName(string i_name);
    List<ItemDef> ListAllManaItems();
    List<RuneDef> ListAllRunes();
}

public class Database: IDatabase
{
    public Database()
    {
        m_itemsByName = new Dictionary<string, ItemDef>();
        m_runesByName = new Dictionary<string, RuneDef>();
        //Carga
        ItemDef[] allItems = Resources.LoadAll<ItemDef>("Database/Items/");
        foreach(ItemDef item in allItems)
        {
            m_itemsByName.Add(item.name, item);
        }

        RuneDef[] allRunes = Resources.LoadAll<RuneDef>("Database/Runes/");
        foreach (RuneDef rune in allRunes)
        {
            m_runesByName.Add(rune.name, rune);
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

    public List<RuneDef> ListAllRunes()
    {
        List<RuneDef> allRunes = new List<RuneDef>();
        foreach (RuneDef rune in m_runesByName.Values)
        {
            allRunes.Add(rune);
        }
        return allRunes;
    }

    Dictionary<string, ItemDef> m_itemsByName;
    Dictionary<string, RuneDef> m_runesByName;
}
