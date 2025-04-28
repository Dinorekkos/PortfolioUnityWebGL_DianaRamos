using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DINO
{
    [CreateAssetMenu(fileName = "ProjectData", menuName = "DINO/ProjectData", order = 0)]
    public class ProjectData : ScriptableObject
    {
        [SerializeField] private string projectName;
        [SerializeField] private string studioName;
        [SerializeField] private string description;
        [SerializeField] private string job;
        [SerializeField] private Sprite image;
        
        [Header("Aspect Ratio Fitter")]
        [SerializeField] private float aspectRatioFitterPC = 0; 
        [SerializeField] private AspectRatioFitter.AspectMode aspectModePC;
        
        [SerializeField] private float aspectRatioFitterMobile = 0;
        [SerializeField] private AspectRatioFitter.AspectMode aspectModeMobile;
        [SerializeField] private float posYMainImage = 0;
        
        
        
        [SerializeField] private string link;
        [SerializeField] private Sprite[] screenShots;
        public string ProjectName => projectName;
        public string StudioName => studioName;
        public string Description => description;
        public Sprite Image => image;
        public string Link => link;
        public string Job => job;
        public Sprite[] ScreenShots => screenShots;
        public float AspectRatioFitterPC => aspectRatioFitterPC;
        public AspectRatioFitter.AspectMode AspectModePC => aspectModePC;
        public float AspectRatioFitterMobile => aspectRatioFitterMobile;
        public AspectRatioFitter.AspectMode AspectModeMobile => aspectModeMobile;
        public float PosYMainImage => posYMainImage;
    }
}
