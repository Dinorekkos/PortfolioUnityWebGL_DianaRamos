using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    #region Variables
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 targetRotation;
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 homePosition;
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 cityPosition;
    
    // [BoxGroup("Input")]
    // [AssetsOnly]
    // [SerializeField] InputActionReference cameraInput;

    private Vector2 _cameraDelta;
    
    
    
    #endregion


    #region Unity Methods

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
    #endregion


    #region Public Methods

    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f,1,0.6f)]
    public void DoSmoothEffect()
    {
        ConsoleProDebug.LogToFilter("DoSmoothEffect","Camera");
    }
    #endregion


    #region Private Methods

    public void OnLook(InputAction.CallbackContext context)
    {
        _cameraDelta = context.action.ReadValue<Vector2>();
        ConsoleProDebug.LogToFilter("MousePosition " + _cameraDelta ,"Camera");
        
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _cameraDelta = context.action.ReadValue<Vector2>();
        ConsoleProDebug.LogToFilter("MousePosition " + _cameraDelta ,"Camera");
        
    }
    
    #endregion
}

