using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarCanvas : MonoBehaviour
{
    private Canvas _canvas;
    private Transform cam;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        cam = Camera.main.transform;
        transform.LookAt(cam);
    }

    private void FixedUpdate()
    {
        transform.LookAt(cam);
    }
}