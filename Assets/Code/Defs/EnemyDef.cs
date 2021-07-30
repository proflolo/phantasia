using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Definition", menuName = "Game/Enemy Definition", order = 2)]

public class EnemyDef : ScriptableObject
{
    public GameObject battlePrefab;
}
