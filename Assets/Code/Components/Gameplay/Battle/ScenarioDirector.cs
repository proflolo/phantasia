using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioDirector : MonoBehaviour
{
    List<BattleDeco> m_decoPlaces;
    List<BattleDeco2D> m_decoPlaces2D;

    // Start is called before the first frame update
    void Awake()
    {
        m_decoPlaces = new List<BattleDeco>();
        m_decoPlaces2D = new List<BattleDeco2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap(Biome i_biome)
    {
        //Trabajo
        foreach (BattleDeco decoPlace in m_decoPlaces)
        {
            if (decoPlace.decoType == BattleDeco.DecoType.Tall)
            {
                float treeChance = Random.Range(0, 100);
                if (treeChance <= i_biome.bigDecoChanceInBattle)
                {
                    float rotationDegrees = Random.Range(0, 359);
                    GameObject deco = Instantiate(i_biome.FindBigDecoToSpawn(), decoPlace.transform.position, Quaternion.AngleAxis(rotationDegrees, Vector3.up), decoPlace.transform);
                    float randomScale = Random.Range(1.1f, 1.3f);
                    deco.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                }
            }
            else
            {
                float treeChance = Random.Range(0, 100);
                if (treeChance <= i_biome.bigDecoChanceInBattle)
                {
                    float rotationDegrees = Random.Range(0, 359);
                    GameObject deco = Instantiate(i_biome.FindBigDecoToSpawn(), decoPlace.transform.position, Quaternion.AngleAxis(rotationDegrees, Vector3.up), decoPlace.transform);
                    float randomScale = Random.Range(1.1f, 1.3f);
                    deco.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                }
            }
        }

        foreach (BattleDeco2D decoPlace in m_decoPlaces2D)
        {
            float decoChance = Random.Range(0, 100);
            float pickedDeco = Random.Range(0, 100);
            BattleDeco2D.DecoType decoType = decoPlace.decoType;
            switch(decoType)
            {
                case BattleDeco2D.DecoType.Big:
                    if(decoChance > i_biome.bigDecoChanceInBattle)
                    {
                        decoPlace.SetSprite(i_biome.def.background_big[Random.Range(0, i_biome.def.background_big.Length)]);
                    }
                    else
                    {
                        decoPlace.SetSprite(null);
                    }
                    break;
                case BattleDeco2D.DecoType.Small:
                    if (decoChance > i_biome.smallDecoChanceInBattle)
                    {
                        decoPlace.SetSprite(i_biome.def.background_small[Random.Range(0, i_biome.def.background_small.Length)]);
                    }
                    else
                    {
                        decoPlace.SetSprite(null);
                    }
                    break;
                case BattleDeco2D.DecoType.Far:
                    decoPlace.SetSprite(i_biome.def.background_far[Random.Range(0, i_biome.def.background_far.Length)]);
                    break;
            }
        }
    }

    public void AddBattleDeco(BattleDeco i_deco)
    {
        m_decoPlaces.Add(i_deco);
    }

    public void AddBattleDeco(BattleDeco2D i_deco)
    {
        m_decoPlaces2D.Add(i_deco);
    }
}
