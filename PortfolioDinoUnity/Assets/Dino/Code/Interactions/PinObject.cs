using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinObject : BuildingObject
{
    [SerializeField] private Image pinImage;
    void Start()
    {
        pinImage.sprite = ProjectData.Image;
    }

    
}
