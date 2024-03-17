using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed;
    public Vector2 minMaxZoom;
    private Camera _cam;
    private float _targetZoom;

    private void Awake()
    {
        _cam = Camera.main;
        _cam.orthographicSize = minMaxZoom.x;
    }

    private void Update()
    {
        _targetZoom = Mathf.Clamp(_targetZoom += -Input.mouseScrollDelta.y, minMaxZoom.x, minMaxZoom.y);
        if (_cam.orthographicSize != _targetZoom)
        {
            _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, _targetZoom, zoomSpeed * Time.deltaTime);
        }
    }
}
