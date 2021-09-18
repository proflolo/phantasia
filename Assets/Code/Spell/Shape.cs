using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape
{
    public struct Params
    {
        public GameObject caster;
        public BattleWorld world;
        public Vector3 position;
        public Vector3 normal;
    }
    public abstract void CreateFor(Params i_caster);
}
