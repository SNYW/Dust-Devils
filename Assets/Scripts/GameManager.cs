using System;
using ObjectPooling;
using SystemEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask mouseMask;
    public Vehicle test;
    void Awake()
    {
        test = FindObjectOfType<Vehicle>();
        SystemEventManager.Init();
        ObjectPoolManager.InitPools();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.GameStart, null);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && MouseManager.GetMousePositionOnNavmesh(mouseMask, test.transform.position, out var pos ))
        {
            test.SetMoveGoal(pos);
        }
    }
}
