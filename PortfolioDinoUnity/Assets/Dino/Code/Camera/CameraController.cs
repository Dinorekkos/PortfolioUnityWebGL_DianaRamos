using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using DINO;

public class CameraController : MonoBehaviour
{
    #region Variables
    [TabGroup("Camera Transitions")]
    [SerializeField] private Vector3 homePosition;
    [TabGroup("Camera Transitions")]
    [SerializeField] private Vector3 cityPosition;
    
    [TabGroup("Camera Settings")]
    [SerializeField] private float rotationSpeed = 0.5f;
    [TabGroup("Camera Settings")]
    [SerializeField] private float transitionSpeed = 1f;
    
    [TabGroup("Camera States")]
    [SerializeField] private CameraStates cameraState;
    [TabGroup("Camera Max Positions")]
    
    public static CameraController Instance;
    
    private Vector2 _cameraDelta;
    private bool _isCameraRotating;
    private bool _isBusy;

    private float _xRotation;

    #endregion


    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _xRotation = transform.rotation.eulerAngles.x;
        homePosition = transform.position;
        cameraState = CameraStates.Home;
        
       
    }

    private void LateUpdate()
    {
        if (cameraState != CameraStates.City) return;

        // if (_isCameraMoving)
        // {
        //     Vector3 cameraPosition = transform.right * (_cameraDelta.x * -movementSpeed);
        //     cameraPosition += transform.up * (_cameraDelta.y * -movementSpeed);
        //     transform.position += cameraPosition * Time.deltaTime;
        // }

        if (_isCameraRotating)
        {
            transform.Rotate(new Vector3(_xRotation,-_cameraDelta.x * rotationSpeed , 0.0f));
            transform.rotation =  Quaternion.Euler(_xRotation,transform.rotation.eulerAngles.y,0.0f);
        }
        
    }

    
    
    #endregion

    
    
    #region Public Methods
    
    public void ChangeCameraTo(CameraStates state)
    {
        cameraState = state;
        
        Vector3 targetPosition = cameraState switch
        {
            CameraStates.Home => homePosition,
            CameraStates.City => cityPosition,
            
        };    
        DoTransition(targetPosition);
        
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _cameraDelta = context.action.ReadValue<Vector2>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        // _isCameraMoving = context.started || context.performed;
    }
    
    public void OnRotate(InputAction.CallbackContext context)
    {
        if (_isBusy) return;

        _isCameraRotating = context.started || context.performed;
        
        if (context.canceled)
        { 
            _isBusy = true; 
            SnappedRotation();
        }
    }
    
    #endregion
    
    #region Private Methods
    
    private void  SnappedRotation()
    {
        transform.DORotate(SnappedVector(), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            _isBusy = false;
        });
    }
    
    private Vector3 SnappedVector()
    {
        var endValue = 0.0f;
        var currentY = Mathf.Ceil(transform.rotation.eulerAngles.y);

        endValue = currentY switch
        {
            >= 0 and <= 90 => 45.0f,
            >= 91 and <= 180 => 135.0f,
            >= 181 and <= 270 => 225.0f,
            _ => 315.0f
        };

        return new Vector3(_xRotation, endValue, 0.0f);
    }
    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f,1,0.6f)]
    private void DoTransition(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, transitionSpeed).SetEase(Ease.OutQuad).OnComplete(() =>
            ChangeCameraTo(CameraStates.City));
    }
   
    
    #endregion
}

