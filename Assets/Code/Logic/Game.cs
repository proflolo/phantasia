using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private static Game s_instance = null;

    static public Game instance
    {
        get
        {
            return GetInstance();
        }
    }

    static public Game GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = new Game();
        }

        return s_instance;
    }

    private Game()
    {
        m_character = new Character();
        m_inventory = new Inventory();

        Savedelegate savedelegate = new Savedelegate();
        Savedelegate.LoadResult result = savedelegate.Load(out m_data, out m_character.m_data, out m_inventory.m_data);
        if(result == Savedelegate.LoadResult.Failed)
        {
            //Datos por defecto!
            m_character = new Character();
            m_data = new SaveData();
        }
    }

    public ICharacter character
    {
        get
        {
            return m_character;
        }
    }

    public void AddXP(int i_xp)
    {
        m_character.AddXP(i_xp);
        //Guardar!
        Save();
    }

    public void AddItem(ItemDef i_item, uint i_itemAmount)
    {
        //Toma decisiones
        bool accepted = true; //Calculo complejo de economia de juego
        if(accepted)
        {
            m_inventory.AddItem(i_item, i_itemAmount);
            Save();
        }
    }

    private void Save()
    {
        //Codigo aqui
        //NO!
        Savedelegate saveDelegate = new Savedelegate();
        saveDelegate.Save(m_data, m_character.m_data, m_inventory.m_data);
    }

    private Character m_character;
    private Inventory m_inventory;

    public struct SaveData
    {
        

    };

    SaveData m_data;


}
