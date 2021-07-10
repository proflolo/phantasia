using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Savedelegate
{
    static int k_verion = 1;
    public void Save(Game.SaveData i_gameSaveData, Character.SaveData i_characterSaveData)
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/data.bin", FileMode.OpenOrCreate);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(k_verion);
        writer.Write(i_gameSaveData.numTimesXPGiven);
        writer.Write(i_characterSaveData.xp);
        writer.Write(i_characterSaveData.level);
        writer.Close();
        file.Close();
        //Application.temporaryCachePath;
    }

    public enum LoadResult
    {
        Ok,
        Failed
    }
    public LoadResult Load(out Game.SaveData o_saveData, out Character.SaveData o_characterSaveData)
    {
        o_saveData = new Game.SaveData();
        o_characterSaveData = new Character.SaveData();

        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/data.bin", FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            int version = reader.ReadInt32();
            if (version == k_verion)
            {
                o_saveData.numTimesXPGiven = reader.ReadInt32();
                o_characterSaveData.xp = reader.ReadInt32();
                o_characterSaveData.level = reader.ReadInt32();
            }
            reader.Close();
            file.Close();
            return LoadResult.Ok;
        }
        catch (Exception e)
        {
            Debug.Assert(false, e.Message);
            return LoadResult.Failed;
        }
        finally
        {
        }


    }
}
