using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

namespace DINO
{ 
    public class HomeController : MonoBehaviour
    {
        public static HomeController Instance;

        [SerializeField] private GameObject clouds;
        
        bool isInHome = true;
        bool isInCity = false;
        
        void Start()
        {
            Instance = this;
        }
        
        
        public void GoToCity()
        {
            CameraController.Instance.ChangeCameraTo(CameraStates.City);
            StartCoroutine(DelayClouds());
        }
        public void GoToHome()
        {
            CameraController.Instance.ChangeCameraTo(CameraStates.Home);
            EnableClouds(true);
        }
        
        private void EnableClouds(bool enable)
        {
          clouds.SetActive(enable);
        }
       
        IEnumerator DelayClouds()
        {
           yield return new WaitForSeconds(0.5f);
           EnableClouds(false);
        }
    
    }
}

