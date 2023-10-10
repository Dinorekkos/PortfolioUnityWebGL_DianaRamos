using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortfolioInitializer : MonoBehaviour
{
    public static PortfolioInitializer Instance;

    public bool IsMobile
    {
        get => _isMobile;
        protected set=> _isMobile = value;
    }


    private bool _isMobile;
    
    
    void Start()
    {
        Instance = this;
        _isMobile = Application.isMobilePlatform;
    }

  
    
    
    
}
