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
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI projectName;
    [SerializeField] private TextMeshProUGUI studioName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI job;
    [SerializeField] private Image image;
    
    [Header("Animation")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform projectUIPC;
    [SerializeField] private Transform projectUIPhones;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;
    
    #endregion
    
    bool _isMobile = false;
    bool _isShowing = false;
    void Start()
    {
        _isMobile = PortfolioInitializer.Instance.IsMobile;
        canvasGroup.alpha = 0; 
        projectUIPhones.DOScale(Vector3.zero, 0);
        projectUIPC.DOScale(Vector3.zero,0);
    }

    
    
    public void SetInfo(ProjectData data)
    {
        projectData = data;
        projectName.text = projectData.ProjectName;
        studioName.text = projectData.StudioName;
        description.text = projectData.Description;
        job.text = projectData.Job;
        image.sprite = projectData.Image;
    }
    
    public void ShowProjectUI()
    {
        canvasGroup.alpha = 1;
        if (_isShowing) return;
        _isShowing = true;
        if (_isMobile)
        {
            projectUIPhones.gameObject.SetActive(true);
            projectUIPhones.DOScale(Vector3.one, duration).SetEase(showEase).SetDelay(delay);
        }
        else
        {
            projectUIPC.gameObject.SetActive(true);
            projectUIPC.DOScale(Vector3.one, duration).SetEase(showEase).SetDelay(delay);
        }
        
    }
    
    public void HideProjectUI()
    {
        _isShowing = false;
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


#if UNITY_EDITOR_WIN
[CustomEditor(typeof( ProjectUI))]
public class ProjectUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ProjectUI myScript = (ProjectUI) target;
        if (GUILayout.Button("Show Project UI"))
        {
            myScript.ShowProjectUI();
        }
        if (GUILayout.Button("Hide Project UI"))
        {
            myScript.HideProjectUI();
        }
    }
}



#endif