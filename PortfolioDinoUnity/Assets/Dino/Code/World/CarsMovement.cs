using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CarsMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] path;
    [SerializeField] private float duration = 10f;
    [SerializeField] private Transform carVisual;
    [SerializeField] private float addedRotation = 90f;
    [SerializeField] private int ignoreWaypoint = 5;
    
    private float _currentCarDirection = 0f;
    void Start()
    {
        transform.DOLocalPath(path, duration).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).OnWaypointChange((i)=>
        {
            Rotate = i;
        });
    }

    private int Rotate
    {
        set
        {
            //Ignore first waypoint a
            if (value == 0 || value == ignoreWaypoint)
            {
                return;
            }

            _currentCarDirection = carVisual.localRotation.eulerAngles.y;
            _currentCarDirection += addedRotation;
            carVisual.DORotate(new Vector3(0, _currentCarDirection, 0), 0.7f);
        }
    }
}
