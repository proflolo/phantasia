using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    public abstract void ApplyImpact(Vector3 i_direction, float i_force);
}
