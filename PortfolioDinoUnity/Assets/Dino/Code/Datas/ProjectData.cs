using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private string link;
        [SerializeField] private Sprite[] screenShots;
        public string ProjectName => projectName;
        public string StudioName => studioName;
        public string Description => description;
        public Sprite Image => image;
        public string Link => link;
        public string Job => job;
        public Sprite[] ScreenShots => screenShots;
    }
}
