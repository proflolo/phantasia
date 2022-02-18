using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome
{
    public BiomeDef def;
    public List<GameObject> bigDecos;
    public int bigDecoChanceInExploration;
    public int smallDecoChanceInExploration;
    public int bigDecoChanceInBattle;
    public int smallDecoChanceInBattle;
    public int collectibleChance;

    public GameObject FindBigDecoToSpawn()
    {
        return bigDecos[0]; //TODO, comprobar si hay al menos una
    }

    public GameObject FindSmallDecoToSpawn()
    {
        return bigDecos[0]; //TODO, comprobar si hay al menos una
    }
}
