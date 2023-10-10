using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DINO;
using UnityEngine;
using UnityEngine.UI;

public class CityUI : MonoBehaviour
{
    #region Serialized Fields
    
    [SerializeField] private CanvasGroup cityCanvasGroup;
    [SerializeField] private float delayToShow = 1.5f;
    [SerializeField] private float durationCityFade = 0.7f;
    [SerializeField] private float durationHomeFade = 0.3f;
    [SerializeField] private Image interactionHand;
    #endregion

    #region Unity Methods
    
    void Start()
    {
        CameraController.Instance.OnCompleteTransition += HandleUIVisible;
        interactionHand.transform.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    
    #endregion

    #region private methods
    private void StartTransition()
    {
        StartCoroutine(DelayToShow());
    }
    private IEnumerator DelayToShow()
    {
        yield return new WaitForSeconds(1.5f);
        cityCanvasGroup.DOFade(1, durationCityFade);
    }
    private void HandleUIVisible()
    {
        CameraStates cameraState = CameraController.Instance.CameraState;
        switch (cameraState)
        {
            case CameraStates.Home:
                cityCanvasGroup.DOFade(0, durationHomeFade);
                break;
            case CameraStates.City:
                StartTransition();
                break;
        }
    }
    
    #endregion
}
