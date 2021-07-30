using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Savedelegate
{
    static int k_verion = 2;
    public void Save(Game.SaveData i_gameSaveData, Character.SaveData i_characterSaveData, Inventory.SaveData i_inventorySaveData)
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/data.bin", FileMode.OpenOrCreate);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(k_verion);
        writer.Write(i_characterSaveData.xp);
        writer.Write(i_characterSaveData.level);
        writer.Write(i_inventorySaveData.stock.Count);
        foreach(KeyValuePair<ItemDef, uint> kv in i_inventorySaveData.stock)
        {
            writer.Write(kv.Key.name);
            writer.Write(kv.Value);
        }
        writer.Close();
        file.Close();
        //Application.temporaryCachePath;
    }

    public enum LoadResult
    {
        Ok,
        Failed
    }
    public LoadResult Load(Database i_database, out Game.SaveData o_saveData, out Character.SaveData o_characterSaveData, out Inventory.SaveData o_inventoryData)
    {
        o_saveData = new Game.SaveData();
        o_characterSaveData = new Character.SaveData();
        o_inventoryData = new Inventory.SaveData();
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/data.bin", FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            int version = reader.ReadInt32();
            if (version == 1)
            {
                reader.ReadInt32();
                o_characterSaveData.xp = reader.ReadInt32();
                o_characterSaveData.level = reader.ReadInt32();
            }
            else if (version == 2)
            {
                o_characterSaveData.xp = reader.ReadInt32();
                o_characterSaveData.level = reader.ReadInt32();
                int numItemsInInventory = reader.ReadInt32();
                for(int i = 0; i < numItemsInInventory; ++i)
                {
                    //Leer un item
                    string itemName = reader.ReadString();
                    uint amount = reader.ReadUInt32();
                    ItemDef item = i_database.FindItemByName(itemName);
                    if(item)
                    {
                        o_inventoryData.stock.Add(item, amount);
                    }
                    else
                    {
                        //qué hago con el item que no existe???
                        Debug.Assert(false, "Item con nombre " + itemName + " no existe en la BD");
                    }
                }
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
