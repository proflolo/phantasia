using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Game/Biome", order = 1)]
public class BiomeDef : ScriptableObject
{
    public Vector2 tileset_0_delta;
    public Vector2 tileset_1_delta;
    public Vector2 tileset_2_delta;
    public Vector2 treeset_delta;
}
