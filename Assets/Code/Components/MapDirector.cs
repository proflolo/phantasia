using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDirector : MonoBehaviour
{
    [SerializeField] GameObject m_pieces;
    [SerializeField] GameObject m_collectible;
    [SerializeField] GameObject m_encounter;

    private void Awake()
    {
        Debug.Assert(m_pieces != null, "Pieces no ha sido asignado");
    }

    enum Height
    {
        High,
        Path
    }
    struct Cell
    {
        public Height value;
        public bool hasTree;
        public uint row;
        public uint col;
        public ItemDef collectible;
        public BattleDef encounter;
    }

    class MapData
    {
        public MapData(IList<IList<bool>> i_map, int i_width, int i_height, Biome i_biome)
        {
            uint colIdx = 0;
            m_cells = new List<List<Cell>>();
            foreach (IList<bool> row in i_map)
            {
                uint rowIdx = 0;
                List<Cell> colData = new List<Cell>();
                foreach (bool value in row)
                {
                    Cell cell = new Cell();
                    if(value)
                    {
                        cell.value = Height.High;
                        //Tirada random de árbol
                        float treeChance = Random.Range(0, 100);
                        if (treeChance <= i_biome.bigDecoChanceInExploration)
                        {
                            cell.hasTree = true;
                        }
                        else
                        {
                            cell.hasTree = false;
                        }
                    }
                    else
                    {
                        cell.value = Height.Path;
                        float collectibleRoll = Random.Range(0, 100);
                        float battleRoll = Random.Range(0, 100);
                        if(collectibleRoll < i_biome.collectibleChance)
                        {
                            cell.collectible = i_biome.def.collectibles[Random.Range(0, i_biome.def.collectibles.Length)];
                        }
                        else if(battleRoll < 30)
                        {
                            cell.encounter = i_biome.def.battles[Random.Range(0, i_biome.def.battles.Length)]; ;
                        }
                    }
                    cell.col = colIdx;
                    cell.row = rowIdx;
                    colData.Add(cell);
                    rowIdx++;
                }
                m_cells.Add(colData);
                colIdx++;
            }
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

        public Cell get(int i_row, int i_column)
        {
            return m_cells[i_column][i_row];
            
        }

        

        public List<List<Cell>> m_cells;
        public int height;
        public int width;
    }

    MapData _GenerateMap(Biome i_biome)
    {
        //Conectar algoritmo de generacion de laberintos
        MapGen mapGen = new MapGen(31, 31);
        mapGen.addExtraPaths(5, true);
        return new MapData(mapGen.getMap(), 31, 31, i_biome);
    }

    MapData m_map;

    public void GenerateMap(Biome i_biome, GameObject i_mainCharacter)
    {
        m_map = _GenerateMap(i_biome);
        InstantiateMap(m_map, i_biome, i_mainCharacter);
    }


    int ConvertToInt(Cell i_cell)
    {
        if(i_cell.value == Height.High)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    Vector3 ComputeCellCenter(Cell i_cell, MapData i_map)
    {
        float posZ = (i_map.height - i_cell.row - 1) * 3.0f - 3.0f;
        float posX = (i_cell.col) * 3.0f - 3.0f;
        return new Vector3(posX, 0.0f, posZ);
    }

    void InstantiateMap(MapData i_mapData, Biome i_biome, GameObject i_mainCharacter)
    {
        float deltaX = 0;
        float deltaZ = ((i_mapData.height - 2) / 2) * 6.0f;
        for (int row = 0; row < i_mapData.height - 2; row += 2)
        {
            for (int col = 0; col < i_mapData.width - 2; col += 2)
            {
                long tileId = ConvertToInt(i_mapData.get(row + 2, col + 2)) * 100000000
                                + ConvertToInt(i_mapData.get(row + 2, col + 1)) * 10000000
                                + ConvertToInt(i_mapData.get(row + 2, col + 0)) * 1000000
                                + ConvertToInt(i_mapData.get(row + 1, col + 2)) * 100000
                                + ConvertToInt(i_mapData.get(row + 1, col + 1)) * 10000
                                + ConvertToInt(i_mapData.get(row + 1, col + 0)) * 1000
                                + ConvertToInt(i_mapData.get(row + 0, col + 2)) * 100
                                + ConvertToInt(i_mapData.get(row + 0, col + 1)) * 10
                                + ConvertToInt(i_mapData.get(row + 0, col + 0)) * 1;
                string pieceName = "piece_" + tileId.ToString("D9");
                Transform childTransform = m_pieces.transform.Find(pieceName);
                Debug.Assert(childTransform != null);
                if (childTransform != null)
                {
                    GameObject tile = Instantiate(childTransform.gameObject, new Vector3(deltaX, 0.0f, deltaZ), Quaternion.AngleAxis(-90.0f, Vector3.right), transform);
                    tile.transform.localScale = new Vector3(1.0f, 1.0f, 4.0f);
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
                Cell cell = i_mapData.get(row, col);
                if (cell.hasTree)
                {
                    Vector3 pos = ComputeCellCenter(cell, i_mapData);
                    float rotationDegrees = Random.Range(0, 359);
                    pos.x += Random.Range(-0.5f, 0.5f);
                    pos.z += Random.Range(-0.5f, 0.5f);
                    pos.y = 1.5f;
                    GameObject deco = Instantiate(i_biome.FindBigDecoToSpawn(), pos, Quaternion.AngleAxis(rotationDegrees, Vector3.up), transform);
                    float randomScale = Random.Range(0.8f, 1.0f);
                    deco.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                    AudioFade fadeComponent = deco.GetComponent<AudioFade>();
                    if(fadeComponent)
                    {
                        fadeComponent.SetTarget(i_mainCharacter);
                    }
                }
                
                if(cell.collectible != null)
                {
                    Vector3 pos = ComputeCellCenter(cell, i_mapData);
                    pos.y = 0.3f;
                    GameObject collectible = Instantiate(m_collectible, pos, Quaternion.identity, transform);
                    CollectibleController collController = collectible.GetComponent<CollectibleController>();
                    Debug.Assert(collController);
                    if(collController)
                    {
                        collController.Config(cell.collectible);
                    }
                }

                if (cell.encounter != null)
                {
                    Vector3 pos = ComputeCellCenter(cell, i_mapData);
                    pos.y = 0.3f;
                    GameObject encounter = Instantiate(m_encounter, pos, Quaternion.identity, transform);
                    EncounterController encounterController = encounter.GetComponent<EncounterController>();
                    Debug.Assert(encounterController);
                    if (encounterController)
                    {
                        encounterController.Config(cell.encounter);
                    }
                }
            }

           
        }

        
    }

    public enum GroundType
    {
        Low,
        High
    }

    public GroundType ComputeGroundType(Vector3 i_position)
    {
       
        //int value = m_map.get(i_position);
        //if (value == 0)
        //{
            return GroundType.High;
        //}
        //else
        //{
        //    return GroundType.High;
        //}
    }

}
