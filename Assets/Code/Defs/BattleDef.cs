using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle Definition", menuName = "Game/Battle Definition", order = 2)]
public class BattleDef : ScriptableObject
{
    public EnemyDef[] enemies;
}
