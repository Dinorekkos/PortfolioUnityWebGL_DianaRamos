using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private GameObject clouds;

    [SerializeField] private Transform cloudsLimit;

    [SerializeField] private float time = 7f;

    void Start()
    {
        MoveClouds();
    }
    
    void MoveClouds()
    {
        clouds.transform.DOMove(cloudsLimit.position, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
