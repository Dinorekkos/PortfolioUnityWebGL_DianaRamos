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
    
    [Header("Mobile")]
    [SerializeField] private TextMeshProUGUI projectName_mobile;
    [SerializeField] private TextMeshProUGUI studioName_mobile;
    [SerializeField] private TextMeshProUGUI description_mobile;
    [SerializeField] private TextMeshProUGUI job_mobile;
    [SerializeField] private Image image_mobile;
    [SerializeField] private Button linkButton_mobile;
    [SerializeField] private TextMeshProUGUI linkText_mobile;
    
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
    void Start()
    {
        _isMobile = PortfolioInitializer.Instance.IsMobile;
        canvasGroup.alpha = 0; 
        projectUIPhones.DOScale(Vector3.zero, 0);
        projectUIPC.DOScale(Vector3.zero,0);
        
        if (_isMobile)
        {
            projectUIPC.gameObject.SetActive(false);
        }
        else
        {
            projectUIPhones.gameObject.SetActive(false);
        }
    }

    
    
    public void SetInfo(ProjectData data)
    {
        if(_isShowing) return;
        projectData = data;
        
        //Mobile canvas
        projectName_mobile.text = projectData.ProjectName;
        studioName_mobile.text = projectData.StudioName;
        description_mobile.text = projectData.Description;
        job_mobile.text = projectData.Job;
        image_mobile.sprite = projectData.Image;
        linkText_mobile.text = projectData.Link;
        linkButton_mobile.onClick.AddListener(() =>
        {
            // Application.OpenURL(projectData.Link);
            Nfynt.NPlugin.OpenURL(projectData.Link);
        });
        
        //PC canvas
        projectName_pc.text = projectData.ProjectName;
        studioName_pc.text = projectData.StudioName;
        description_pc.text = projectData.Description;
        job_pc.text = projectData.Job;
        image_pc.sprite = projectData.Image;
        linkText_pc.text = projectData.Link;
        linkButton_pc.onClick.AddListener(() =>
        {
            // Application.OpenURL(projectData.Link);
            Nfynt.NPlugin.OpenURL(projectData.Link);
        });
    }
    
    public void ShowProjectUI()
    {
        if (_isShowing) return;
        homeUI.EnableUICity(false);
        _isShowing = true;
        if (_isMobile)
        {
            projectUIPhones.gameObject.SetActive(true);
            canvasGroup.alpha = 1;

            projectUIPhones.DOScale(Vector3.one, duration).SetEase(showEase).SetDelay(delay);
        }
        else
        {
            projectUIPC.gameObject.SetActive(true);
            canvasGroup.alpha = 1;

            projectUIPC.DOScale(Vector3.one, duration).SetEase(showEase).SetDelay(delay);
        }
        
    }
    
    public void HideProjectUI()
    {
        _isShowing = false;
        homeUI.EnableUICity(true);
        if (_isMobile)
        {
            projectUIPhones.DOScale(Vector3.zero, duration).SetEase(hideEase).onComplete += () =>
            {
                canvasGroup.alpha = 0;
                projectUIPhones.gameObject.SetActive(false);
            };
        }
        else
        {
            projectUIPC.DOScale(Vector3.zero, duration).SetEase(hideEase).onComplete += () =>
            {
                canvasGroup.alpha = 0;
                projectUIPC.gameObject.SetActive(false);
            };
        }
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