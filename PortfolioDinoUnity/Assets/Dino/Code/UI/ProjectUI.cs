using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DINO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Serialization;

public class ProjectUI : MonoBehaviour
{
    #region SerializedFields
    
    [Header("Project Data")]
    [SerializeField] private ProjectData projectData;
    
    [FormerlySerializedAs("projectName")]
    [Header("PC")]
    [SerializeField] private TextMeshProUGUI projectName_pc;
    [SerializeField] private TextMeshProUGUI studioName_pc;
    [SerializeField] private TextMeshProUGUI description_pc;
    [SerializeField] private TextMeshProUGUI job_pc;
    [SerializeField] private Image image_pc;
    [SerializeField] private Button linkButton_pc;
    [SerializeField] private TextMeshProUGUI linkText_pc;
    [SerializeField] private Image[] screenShots_pc;
    
    [Header("Mobile")]
    [SerializeField] private TextMeshProUGUI projectName_mobile;
    [SerializeField] private TextMeshProUGUI studioName_mobile;
    [SerializeField] private TextMeshProUGUI description_mobile;
    [SerializeField] private TextMeshProUGUI job_mobile;
    [SerializeField] private Image image_mobile;
    [SerializeField] private Button linkButton_mobile;
    [SerializeField] private TextMeshProUGUI linkText_mobile;
    [SerializeField] private Image[] screenShots_mobile;
    
    [Header("Animation")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform projectUIPC;
    [SerializeField] private Transform projectUIPhones;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;
    
    [Header("HomeUI")]
    [SerializeField] private HomeUI homeUI;
    
    #endregion
    
    bool _isMobile = false;
    bool _isShowing = false;
    private AspectRatioFitter _aspectRatioFitter;
    private AspectRatioFitter.AspectMode _defaultMode;
    private float _defaultAspectRatio;
    private Vector2 _defaultAnchoredPosition;
    private void Start()
    {
        _isMobile = PortfolioInitializer.Instance.IsMobile;
        HideBothUIs();
    
        InitializeUIComponents(_isMobile ? projectUIPC : projectUIPhones, _isMobile ? image_mobile : image_pc);
    
        linkButton_mobile.onClick.AddListener(OpenLink);
        linkButton_pc.onClick.AddListener(OpenLink);
    }
    
    private void InitializeUIComponents(Transform inactiveUI, Image targetImage)
    {
        inactiveUI.gameObject.SetActive(false);
        _aspectRatioFitter = targetImage.GetComponent<AspectRatioFitter>();
        //default values aspect ratio 3.47 Mobile Width controls hide Mode Mobile
        //default values aspect ratio 0.86 PC hEIGH cONTROLS WEIDTH Mode PC
        //default values rectTransform anchoredPosition pos y = -10f
        
        _defaultMode = _aspectRatioFitter.aspectMode;
        _defaultAspectRatio = _aspectRatioFitter.aspectRatio;
        _defaultAnchoredPosition = targetImage.rectTransform.anchoredPosition;
    }

    private void HideBothUIs()
    {
        canvasGroup.alpha = 0; 
        projectUIPhones.DOScale(Vector3.zero, 0);
        projectUIPC.DOScale(Vector3.zero,0);
    }

    private void OnDestroy()
    {
        linkButton_mobile.onClick.RemoveListener(OpenLink);
        linkButton_pc.onClick.RemoveListener(OpenLink);
    }
    
    
    public void SetInfo(ProjectData data)
    {
        if(_isShowing) return;
        projectData = data;

        if (_isMobile)
        {
            SetMobileInfo();
        }
        else
        {
            SetPcInfo();
        }
        
    }

    private void SetMobileInfo()
    {
        //Mobile canvas
        projectName_mobile.text = projectData.ProjectName;
        studioName_mobile.text = projectData.StudioName;
        description_mobile.text = projectData.Description;
        job_mobile.text = projectData.Job;
        image_mobile.sprite = projectData.Image;
        linkText_mobile.text = projectData.Link;
        
        for (int i = 0; i < screenShots_mobile.Length; i++)
        {
            if (i < projectData.ScreenShots.Length)
            {
                screenShots_mobile[i].sprite = projectData.ScreenShots[i];
            }
            else
            {
                screenShots_mobile[i].gameObject.SetActive(false);
            }
        }
        _aspectRatioFitter.aspectRatio = _defaultAspectRatio;
        _aspectRatioFitter.aspectMode = _defaultMode;
        image_mobile.rectTransform.anchoredPosition = _defaultAnchoredPosition;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(_aspectRatioFitter.GetComponent<RectTransform>());
        
        if (projectData.AspectRatioFitterMobile != 0) _aspectRatioFitter.aspectRatio = projectData.AspectRatioFitterMobile;
        if (projectData.AspectModeMobile != AspectRatioFitter.AspectMode.None) _aspectRatioFitter.aspectMode = projectData.AspectModeMobile;
        if(projectData.PosYMainImage != 0) image_mobile.rectTransform.anchoredPosition = new Vector2(image_mobile.rectTransform.anchoredPosition.x, projectData.PosYMainImage);
    }

    private void SetPcInfo()
    {
        //PC canvas
        projectName_pc.text = projectData.ProjectName;
        studioName_pc.text = projectData.StudioName;
        description_pc.text = projectData.Description;
        job_pc.text = projectData.Job;
        image_pc.sprite = projectData.Image;
        linkText_pc.text = projectData.Link;
        
        for (int i = 0; i < screenShots_pc.Length; i++)
        {
            if (i < projectData.ScreenShots.Length)
            {
                screenShots_pc[i].sprite = projectData.ScreenShots[i];
            }
            else
            {
                screenShots_pc[i].gameObject.SetActive(false);
            }
        }
        
        _aspectRatioFitter.aspectRatio = _defaultAspectRatio;
        _aspectRatioFitter.aspectMode = _defaultMode;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(_aspectRatioFitter.GetComponent<RectTransform>());
        
        if (projectData.AspectRatioFitterPC != 0) _aspectRatioFitter.aspectRatio = projectData.AspectRatioFitterPC;
        if (projectData.AspectModePC != AspectRatioFitter.AspectMode.None) _aspectRatioFitter.aspectMode = projectData.AspectModePC;
        
    }

    private void OpenLink()
    {
        Nfynt.NPlugin.OpenURL(projectData.Link);
    }


    public void ShowProjectUI()
    {
        if (_isShowing) return;

        homeUI.EnableUICity(false);
        _isShowing = true;

        Transform targetUI = _isMobile ? projectUIPhones : projectUIPC;
        targetUI.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        targetUI.DOScale(Vector3.one, duration).SetEase(showEase).SetDelay(delay);
    }
    
    public void HideProjectUI()
    {
        _isShowing = false;
        homeUI.EnableUICity(true);
        
        _isShowing = false;
        homeUI.EnableUICity(true);

        Transform targetUI = _isMobile ? projectUIPhones : projectUIPC;

        targetUI.DOScale(Vector3.zero, duration).SetEase(hideEase).onComplete += () =>
        {
            canvasGroup.alpha = 0;
            targetUI.gameObject.SetActive(false);
            CameraController.Instance.SetCameraStateCity();
        };
    }
    
}


// #if UNITY_EDITOR_WIN
// [CustomEditor(typeof( ProjectUI))]
// public class ProjectUIEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         ProjectUI myScript = (ProjectUI) target;
//         if (GUILayout.Button("Show Project UI"))
//         {
//             myScript.ShowProjectUI();
//         }
//         if (GUILayout.Button("Hide Project UI"))
//         {
//             myScript.HideProjectUI();
//         }
//     }
// }
// #endif