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
    [SerializeField] private Transform projectUIPC;
    [SerializeField] private Transform projectUIPhones;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private Ease ease = Ease.OutBack;
    #endregion
    
    bool isMobile = false;
    void Start()
    {
        isMobile = Application.isMobilePlatform;
        projectUIPhones.gameObject.SetActive(isMobile);
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
        if (isMobile)
        {
            projectUIPhones.DOScale(Vector3.one, duration).SetDelay(delay).SetEase(ease);
        }
        else
        {
            projectUIPC.DOScale(Vector3.one, duration).SetDelay(delay).SetEase(ease);
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
//     }
// }
//
// #endif