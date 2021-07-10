using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICharacter
{
    int GetLevel();
}

public class Character: ICharacter
{
    public struct SaveData
    {
        public int level;
        public int xp;
    };

    public SaveData m_data;

    public Character()
    {
        m_data = new SaveData();
        m_data.level = 0;
        m_data.xp = 0;
    }

    public int GetLevel()
    {
        return m_data.level;
    }


    public void AddXP(int i_value)
    {
        Debug.Assert(i_value >= 0, "La experienmcia es negativa");
        if(i_value > 0)
        {
            m_data.xp += i_value;
            int lastLevel = m_data.level;
            m_data.level = m_data.xp / 10;
            if (lastLevel != m_data.level)
            {
                //Hemos subido de nivel!
            }
        }
    }



}
