using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeDirector : MonoBehaviour
{

    [SerializeField] BiomeDef[] m_biomes;
    Biome m_currentBiome;
    [SerializeField] GameObject[] m_bigDecos;
    private void Awake()
    {
        Debug.Assert(m_bigDecos.Length > 0, "Al menos pon una big deco en el Biome Director");
        Debug.Assert(m_biomes.Length > 0, "No hay bioma asignado");
    }

    // Start is called before the first frame update
    void Start()
    {
        

       

       
    }

    public Biome GetCurrentBiome()
    {
        Debug.Assert(m_currentBiome != null);
        return m_currentBiome;
    }

    public Biome PrepareBiome()
    {
        int biomeId = Random.Range(0, m_biomes.Length);
        BiomeDef biome = m_biomes[biomeId];

        Shader.SetGlobalFloat("_TileOffset_LV0_X", biome.tileset_0_delta.x);
        Shader.SetGlobalFloat("_TileOffset_LV0_Y", biome.tileset_0_delta.y);
        Shader.SetGlobalFloat("_TileOffset_LV1_X", biome.tileset_1_delta.x);
        Shader.SetGlobalFloat("_TileOffset_LV1_Y", biome.tileset_1_delta.y);
        Shader.SetGlobalFloat("_TileOffset_LV2_X", biome.tileset_2_delta.x);
        Shader.SetGlobalFloat("_TileOffset_LV2_Y", biome.tileset_2_delta.y);
        Shader.SetGlobalFloat("_TreeOffset_X", biome.treeset_delta.x);
        Shader.SetGlobalFloat("_TreeOffset_Y", biome.treeset_delta.y);
        m_currentBiome = new Biome();
        m_currentBiome.def = biome;
        m_currentBiome.bigDecoChanceInBattle = 50; //TODO 
        m_currentBiome.smallDecoChanceInBattle = 50; //TODO 
        m_currentBiome.bigDecoChanceInExploration = 15; //TODO
        m_currentBiome.smallDecoChanceInExploration = 15; //TODO
        m_currentBiome.bigDecos = new List<GameObject>(m_bigDecos); //TODO
        m_currentBiome.collectibleChance = 30; //TODO
        return m_currentBiome;
    }

   


   
    
}
