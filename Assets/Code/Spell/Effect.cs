using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect
{
    public struct Params
    {
        public GameObject caster;
        public GameObject receiver;
        public BattleWorld world;
        public Vector3 startPoint;
        public Vector3 startDirection;
    }

    public abstract uint ComputeCastCost();
    public abstract void ExecuteFor(Params i_params);
}
