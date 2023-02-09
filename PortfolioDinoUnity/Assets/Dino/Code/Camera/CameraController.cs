using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    #region Variables
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 targetRotation;
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 homePosition;
    [TabGroup("Camera Positions")]
    [SerializeField] private Vector3 cityPosition;

    [TabGroup("Camera Settings")] 
    [SerializeField] private float movementSpeed = 10f;
    [TabGroup("Camera Settings")]
    [SerializeField] private float rotationSpeed = 0.5f;
    
    private Vector2 _cameraDelta;
    private bool _isCameraMoving;
    private bool _isCameraRotating;

    private float _xRotation;
    
    
    #endregion


    #region Unity Methods

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void LateUpdate()
    {
        if (_isCameraMoving)
        {
            Vector3 cameraPosition = transform.right * (_cameraDelta.x * -movementSpeed);
            // cameraPosition += transform.up * (_cameraDelta.y * -movementSpeed); Hay que clampear a un rango
            transform.position += cameraPosition * Time.deltaTime;
        }

        if (_isCameraRotating)
        {
            transform.Rotate(new Vector3(_xRotation,-_cameraDelta.x * rotationSpeed , 0.0f));
            transform.rotation =  Quaternion.Euler(_xRotation,transform.rotation.eulerAngles.y,0.0f);

        }
    }

    private void Awake()
    {
        _xRotation = transform.rotation.eulerAngles.x;
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
        // ConsoleProDebug.LogToFilter("MousePosition= " + _cameraDelta ,"Camera");
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _isCameraMoving = context.started || context.performed;
        // ConsoleProDebug.LogToFilter("Camera is Moving= " + _isCameraMoving ,"Camera");
    }
    
    public void OnRotate(InputAction.CallbackContext context)
    {
        _isCameraRotating = context.started || context.performed;
        // ConsoleProDebug.LogToFilter("Camera is Rotating= " + _isCameraRotating ,"Camera");
    }
    
    
    #endregion
}

