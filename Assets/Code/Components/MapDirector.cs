using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDirector : MonoBehaviour
{
    [SerializeField] GameObject m_pieces;

    private void Awake()
    {
        Debug.Assert(m_pieces != null, "Pieces no ha sido asignado");
    }

    class MapData
    {
        public MapData(IList<IList<bool>> i_map, int i_width, int i_height)
        {
            m_cells = i_map;
            width = i_width;
            height = i_height;
            //m_cells = new int[][]
            //    {
            //        new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            //        new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            //        new int[] { 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1 },
            //        new int[] { 1, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1 },
            //        new int[] { 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
            //        new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            //        new int[] { 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
            //        new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            //        new int[] { 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1 },
            //        new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
            //        new int[] { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            //    };
        }

        public int get(int i_row, int i_column)
        {
            bool value = m_cells[i_column][i_row];
            if (value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public IList<IList<bool>> m_cells;
        public int height;
        public int width;
    }

    MapData _GenerateMap()
    {
        //Conectar algoritmo de generacion de laberintos
        MapGen mapGen = new MapGen(31, 31);
        mapGen.addExtraPaths(50, true);
        return new MapData(mapGen.getMap(), 31, 31);
    }

    MapData m_map;

    public void GenerateMap(Biome i_biome)
    {
        m_map = _GenerateMap();
        InstantiateMap(m_map, i_biome);
    }


    void InstantiateMap(MapData i_mapData, Biome i_biome)
    {
        float deltaX = 0;
        float deltaZ = ((i_mapData.height - 2) / 2) * 6.0f;
        for (int row = 0; row < i_mapData.height - 2; row += 2)
        {
            for (int col = 0; col < i_mapData.width - 2; col += 2)
            {
                long tileId = i_mapData.get(row + 2, col + 2) * 100000000
                                + i_mapData.get(row + 2, col + 1) * 10000000
                                + i_mapData.get(row + 2, col + 0) * 1000000
                                + i_mapData.get(row + 1, col + 2) * 100000
                                + i_mapData.get(row + 1, col + 1) * 10000
                                + i_mapData.get(row + 1, col + 0) * 1000
                                + i_mapData.get(row + 0, col + 2) * 100
                                + i_mapData.get(row + 0, col + 1) * 10
                                + i_mapData.get(row + 0, col + 0) * 1;
                string pieceName = "piece_" + tileId.ToString("D9");
                Transform childTransform = m_pieces.transform.Find(pieceName);
                Debug.Assert(childTransform != null);
                if (childTransform != null)
                {
                    Instantiate(childTransform.gameObject, new Vector3(deltaX, 0.0f, deltaZ), Quaternion.AngleAxis(-90.0f, Vector3.right), transform);
                }
                deltaX += 6.0f;
            }
            deltaX = 0.0f;
            deltaZ -= 6.0f;
        }

        int numRows = i_mapData.height;
        for (int row = 0; row < numRows; row += 1)
        {
            int numCols = i_mapData.width;
            for (int col = 0; col < numCols; col += 1)
            {
                if (i_mapData.get(row, col) == 1)
                {
                    float posZ = (numRows - row - 1) * 3.0f - 3.0f;
                    float posX = (col) * 3.0f - 3.0f;
                    float rotationDegrees = Random.Range(0, 359);
                    float treeChance = Random.Range(0, 100);
                    if (treeChance <= i_biome.bigDecoChanceInExploration)
                    {
                        float xDelta = Random.Range(-0.5f, 0.5f);
                        float zDelta = Random.Range(-0.5f, 0.5f);
                        GameObject deco = Instantiate(i_biome.FindBigDecoToSpawn(), new Vector3(posX + xDelta, 0.4f, posZ + zDelta), Quaternion.AngleAxis(rotationDegrees, Vector3.up), transform);
                        float randomScale = Random.Range(0.8f, 1.0f);
                        deco.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                    }
                }
            }
        }
    }


}
