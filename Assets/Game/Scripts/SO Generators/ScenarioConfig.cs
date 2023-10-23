using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScenarioConfig : ScriptableObject
{
    public float speed;
    public Vector3 moveDirection;
    public float cycleLength;
    public float stopDistance;
    public bool isMoving;
    public float cityDistance;

    public float Speed { get { return speed; } }
    public Vector3 MoveDirection { get { return moveDirection; } }
    public float CycleLength { get { return cycleLength; } }
    public float StopDistance { get { return stopDistance; } }
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public float CityDistance { get { return cityDistance; } }
}
