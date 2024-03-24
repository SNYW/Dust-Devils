using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vehicle : MonoBehaviour
{
    private VehicleMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<VehicleMovement>();
    }

    public void Init(Vector2 pos)
    {
        _movement.Init(pos);
    }
}
