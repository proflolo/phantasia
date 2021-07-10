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

        Savedelegate savedelegate = new Savedelegate();
        Savedelegate.LoadResult result = savedelegate.Load(out m_data, out m_character.m_data);
        if(result == Savedelegate.LoadResult.Failed)
        {
            //Datos por defecto!
            m_character = new Character();
            m_data = new SaveData();
            m_data.numTimesXPGiven = 0;
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
        m_data.numTimesXPGiven++;
        //Guardar!
        Save();
    }

    private void Save()
    {
        //Codigo aqui
        //NO!
        Savedelegate saveDelegate = new Savedelegate();
        saveDelegate.Save(m_data, m_character.m_data);
    }

    private Character m_character;

    public struct SaveData
    {
        public int numTimesXPGiven;

    };

    SaveData m_data;


}
