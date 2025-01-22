using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class ButtonAudio : MonoBehaviour
{
    [SerializeField] string clickAudio = "Click";
    [SerializeField] string hoverAudio = "Hover";
    private Button button;
    private EventTrigger eventTrigger;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickAudio);
        
        eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnHoverAudio(); });
    }
    
    private void ClickAudio()
    {
        AudioManager.Instance.PlayAudio(clickAudio);
    }
    
    private void OnHoverAudio()
    {
        AudioManager.Instance.PlayAudio(hoverAudio);
    }
}
