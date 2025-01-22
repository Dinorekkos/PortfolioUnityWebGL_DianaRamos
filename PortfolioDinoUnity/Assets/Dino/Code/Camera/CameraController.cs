using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using DINO;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    #region Variables
    [Header("Inputs")]
    [SerializeField] private InputActionReference zoomPrimaryPosInputAction;
    [SerializeField] private InputActionReference zoomSecondaryPosInputAction;
    [SerializeField] private InputActionReference zoomSecondaryTouchInputAction;
    
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    
    [TabGroup("Camera Transitions")]
    [SerializeField] private Vector3 homePosition;
    [TabGroup("Camera Transitions")]
    [SerializeField] private Vector3 cityPosition;
    [TabGroup("Camera Transitions")]
    [SerializeField] private float transitionSpeed = 1f;
    

    [TabGroup("Camera Settings")]
    [SerializeField] private float zoom = 1f; 
    [TabGroup("Camera Settings")]
    [SerializeField] private float movementSpeed = 0.5f;
    
    [TabGroup("Camera States")]
    [SerializeField] private CameraStates cameraState;
    [TabGroup("Camera Max Positions")]
    
    public static CameraController Instance;
    public CameraStates CameraState => cameraState;
    public event Action OnCompleteTransition;
    public event Action OnStartTransition;
    public Vector2 CameraDelta => _cameraDelta;

    private Vector2 _cameraDelta;
    private bool _isCameraRotating;
    private bool _isBusy;
    private bool _isCameraMoving;
    private Vector3 _initialRotation;
    
    private bool _isCameraZooming;
    private Vector2 _primaryTouchPosition = Vector2.zero;
    private Vector2 _secondaryTouchPosition = Vector2.zero;

    private float _xRotation;
    
    private bool _cameraIsTransitioning;
    
    #endregion

    #region Public Variables

    public bool IsCameraRotating => _isCameraRotating;
    public bool IsCameraMoving => _isCameraMoving;
    public bool IsCameraZooming => _isCameraZooming;
    

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
       zoomPrimaryPosInputAction.action.Enable();
       zoomSecondaryPosInputAction.action.Enable();
       zoomSecondaryTouchInputAction.action.Enable();
    }

    private void OnDisable()
    {
        zoomPrimaryPosInputAction.action.Disable();
        zoomSecondaryPosInputAction.action.Disable();
        zoomSecondaryTouchInputAction.action.Disable();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeZoomInputs();
        
        _xRotation = transform.rotation.eulerAngles.x;
        homePosition = transform.position;
        cameraState = CameraStates.Home;
        _initialRotation = transform.rotation.eulerAngles; 
        
    }

    private void LateUpdate()
    {
        if (cameraState != CameraStates.City) return;
        if(_cameraIsTransitioning) return;

        //Drag Camera
        if (_isCameraMoving)
        {
            Vector3 cameraPosition = transform.right * (_cameraDelta.x * -movementSpeed);
            cameraPosition += transform.up * (_cameraDelta.y * -movementSpeed);
            transform.position += cameraPosition * Time.deltaTime;
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
        
        ResetCameraRotation();
        DoTransition(targetPosition);
        
        
    }

    #region Input
    public void OnLook(InputAction.CallbackContext context)
    {
        _cameraDelta = context.action.ReadValue<Vector2>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _isCameraMoving = context.started || context.performed;
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
    
    
    #endregion
    
    #region Private Methods

    private void InitializeZoomInputs()
    {
        if(!PortfolioInitializer.Instance.IsMobile) return;
        
        zoomPrimaryPosInputAction.action.performed += context =>
        {
            _primaryTouchPosition = context.ReadValue<Vector2>();
        };
        zoomSecondaryPosInputAction.action.performed += context =>
        {
            _secondaryTouchPosition = context.ReadValue<Vector2>();
        };
        
        zoomSecondaryTouchInputAction.action.started += context =>
        {
            ZoomStart();
        };
        zoomSecondaryTouchInputAction.action.canceled += context =>
        {
            ZoomFinished();
        };
    }
    private void ZoomStart()
    {
        _isCameraZooming = true;
        StartCoroutine(DoZoom());
    }
    private void ZoomFinished()
    {
        _isCameraZooming = false;
        StopCoroutine(DoZoom());
    }
    private IEnumerator DoZoom()
    {
        float previousDistance = 0f;
        float distance = 0f;
        zoom = 0.1f;

        while (_isCameraZooming)
        {
            distance = Vector2.Distance(_primaryTouchPosition, _secondaryTouchPosition);
            //Zoom Out
            if (distance > previousDistance)
            {
                mainCamera.orthographicSize -= zoom;
            }
            //Zoom In
            else if (distance < previousDistance)
            {
                mainCamera.orthographicSize += zoom;
            }
            previousDistance = distance;
            yield return null;
        }

    }
    private void ResetCameraRotation()
    {
        transform.rotation = Quaternion.Euler(_initialRotation);
    }
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
    
    private void DoTransition(Vector3 targetPosition)
    {
        _cameraIsTransitioning = true;
        OnStartTransition?.Invoke();
        transform.DOMove(targetPosition, transitionSpeed / 2).SetEase(Ease.InOutCubic).OnComplete(() =>
        { 
            OnCompleteTransition?.Invoke(); 
            _cameraIsTransitioning = false; 
        });
    }

    #endregion

    #region Test

    [Button]
    private void TransitionTest()
    {
        ChangeCameraTo(CameraStates.City);
    }
    
    #endregion
}

