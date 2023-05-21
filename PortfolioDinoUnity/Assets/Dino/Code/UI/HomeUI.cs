using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DINO
{
    public class HomeUI : MonoBehaviour 
    {
        [TabGroup("Buttons")]
        [SerializeField] private Button _cityButton;
        
        void Start()
        {
            _cityButton.onClick.AddListener(() => CameraController.Instance.ChangeCameraTo(CameraStates.City));
        }

        void Update()
        {
            
        }
    }
}
