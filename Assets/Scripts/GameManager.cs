using System;
using ObjectPooling;
using SystemEvents;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask unitPlacementMask;
    
    public Vehicle test;
    void Awake()
    {
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
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, unitPlacementMask);
          
            if (hit.collider != null)
            {
                Instantiate(test, hit.point, quaternion.identity).Init(hit.point);
            }
        }
    }
}
