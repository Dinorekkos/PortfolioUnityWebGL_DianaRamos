using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PinAnimation : MonoBehaviour
{
    [SerializeField] private Transform pin;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float jumpPower = 0.5f;
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private float highJump = 0.01f;
    [SerializeField] private Ease ease = Ease.InQuad;
    void Start()
    {
        
        //  do local 2 little jumps on the y axis
        pin.DOLocalJump(new Vector3(pin.transform.localPosition.x,pin.transform.localPosition.y + highJump,pin.transform.localPosition.z),
            jumpPower, jumpCount, duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        
    }

   
    
         
}
