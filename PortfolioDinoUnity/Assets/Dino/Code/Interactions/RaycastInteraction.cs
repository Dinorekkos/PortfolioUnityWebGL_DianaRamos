using System.Collections;
using System.Collections.Generic;
using DINO;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask layerInteractions;
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] CameraController cameraController;
    [SerializeField] private InputActionReference mousePosition;
    
    [Header("ProjectUI")]
    [SerializeField] private ProjectUI projectUI;
    
    private Vector2 _senderPosition;
    private bool _isInteracting = false;

    public void OnInteraction(InputAction.CallbackContext context)
    {

        if(cameraController.CameraState != CameraStates.City) return;
        
        _senderPosition = mousePosition.action.ReadValue<Vector2>();
        _isInteracting = context.started || context.performed;
        
        if (_isInteracting)
        {
            SendRaycastFromMousePos();
        }
        
    }


    private void SendRaycastFromMousePos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_senderPosition);
        
        
        if (Physics.Raycast(ray, out hit, raycastDistance, layerInteractions))
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1f);
            if (hit.collider.gameObject.CompareTag("Project"))
            {
                BuildingObject building = hit.collider.gameObject.GetComponent<BuildingObject>();
                
                if(CameraController.Instance.IsCameraZooming || CameraController.Instance.IsCameraZooming) return;
                projectUI.SetInfo(building.ProjectData);
                projectUI.ShowProjectUI();
                CameraController.Instance.SetCameraStateBuilding();
                // Debug.Log("Showing Project UI");
            }
        }
    }
}
