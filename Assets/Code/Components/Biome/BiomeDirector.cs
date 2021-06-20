using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeDirector : MonoBehaviour
{
    [SerializeField] GameObject m_pieces;
    [SerializeField] GameObject[] m_bigDecos;
    [SerializeField] BiomeDef[] m_biomes;
    class MapData
    {
        public MapData()
        {
            m_cells = new int[][]
                {
                    new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    new int[] { 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1 },
                    new int[] { 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1 },
                    new int[] { 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
                    new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    new int[] { 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
                    new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    new int[] { 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1 },
                    new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                    new int[] { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
                };
        }

        public int[][] m_cells;

    }

    private void Awake()
    {
        Debug.Assert(m_pieces != null, "Pieces no ha sido asignado");
        Debug.Assert(m_biomes.Length > 0, "No hay bioma asignado");
    }

    // Start is called before the first frame update
    void Start()
    {
        int biomeId = Random.Range(0, m_biomes.Length);
        BiomeDef biome = m_biomes[biomeId];

        m_map = GenerateMap();
        InstantiateMap(m_map);

        Shader.SetGlobalFloat("_TileOffset_LV0_X", biome.tileset_0_delta.x);
        Shader.SetGlobalFloat("_TileOffset_LV0_Y", biome.tileset_0_delta.y);
        Shader.SetGlobalFloat("_TileOffset_LV1_X", biome.tileset_1_delta.x);
        Shader.SetGlobalFloat("_TileOffset_LV1_Y", biome.tileset_1_delta.y);
        Shader.SetGlobalFloat("_TileOffset_LV2_X", biome.tileset_2_delta.x);
        Shader.SetGlobalFloat("_TileOffset_LV2_Y", biome.tileset_2_delta.y);
        Shader.SetGlobalFloat("_TreeOffset_X", biome.treeset_delta.x);
        Shader.SetGlobalFloat("_TreeOffset_Y", biome.treeset_delta.y);
    }

    MapData GenerateMap()
    {
        return new MapData();
    }

    void InstantiateMap(MapData i_mapData)
    {
        float deltaX = 0;
        float deltaZ = ((i_mapData.m_cells.Length - 2) / 2) * 6.0f;
        for (int row = 0; row < i_mapData.m_cells.Length - 2; row += 2)
        {
            for (int col = 0; col < i_mapData.m_cells[row].Length - 2; col += 2)
            {
                long tileId =       i_mapData.m_cells[row+2][col+2] * 100000000
                                +   i_mapData.m_cells[row+2][col+1] * 10000000
                                +   i_mapData.m_cells[row+2][col+0] * 1000000
                                +   i_mapData.m_cells[row+1][col+2] * 100000
                                +   i_mapData.m_cells[row+1][col+1] * 10000
                                +   i_mapData.m_cells[row+1][col+0] * 1000
                                +   i_mapData.m_cells[row+0][col+2] * 100
                                +   i_mapData.m_cells[row+0][col+1] * 10
                                +   i_mapData.m_cells[row+0][col+0] * 1;
                string pieceName = "piece_" + tileId.ToString("D9");
                Transform childTransform = m_pieces.transform.Find(pieceName);
                Debug.Assert(childTransform != null);
                if(childTransform != null)
                {
                    Instantiate(childTransform.gameObject, new Vector3(deltaX, 0.0f, deltaZ), Quaternion.AngleAxis(-90.0f, Vector3.right), transform);
                }
                deltaX += 6.0f;
            }
            deltaX = 0.0f;
            deltaZ -= 6.0f;
        }

        int numRows = i_mapData.m_cells.Length;
        for (int row = 0; row < numRows; row += 1)
        {
            int numCols = i_mapData.m_cells[row].Length;
            for (int col = 0; col < numCols; col += 1)
            {
                if (i_mapData.m_cells[row][col] == 1)
                {
                    float posZ = (numRows - row - 1) * 3.0f - 3.0f;
                    float posX = (col) * 3.0f - 3.0f;
                    float rotationDegrees = Random.Range(0, 359);
                    float treeChance = Random.Range(0, 100);
                    if (treeChance < 15)
                    {
                        float xDelta = Random.Range(-0.5f, 0.5f);
                        float zDelta = Random.Range(-0.5f, 0.5f);
                        GameObject deco = Instantiate(m_bigDecos[0], new Vector3(posX + xDelta, 0.4f, posZ + zDelta), Quaternion.AngleAxis(rotationDegrees, Vector3.up), transform);
                        float randomScale = Random.Range(0.8f, 1.0f);
                        deco.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                    }
                }
            }
        }
    }

    public GameObject FindBigDecoToSpawn()
    {
        return m_bigDecos[0];
    }

    MapData m_map;
    
}
