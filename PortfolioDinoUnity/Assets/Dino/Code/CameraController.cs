using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 targetRotation;
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 homePosition;
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 cityPosition;
    
    
    void Start()
    {
    }

    void Update()
    {
        
    }
    
    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f,1,0.6f)]
    public void DoSmoothEffect()
    {
        ConsoleProDebug.LogToFilter("holi","Camera");
    }
}

