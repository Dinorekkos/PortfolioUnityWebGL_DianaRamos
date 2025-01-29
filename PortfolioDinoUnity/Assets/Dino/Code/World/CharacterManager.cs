using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [TabGroup("States")] [SerializeField] private CharacterStates characterState;
    [TabGroup("States")] [SerializeField] private AnimatorHandler animatorHandler;

    [TabGroup("Movement")] [SerializeField] private float duration;
    [TabGroup("Movement")] [SerializeField] private Vector3[] path;
    
    
    
    
    
    void Start()
    {
        ChangeState(CharacterStates.Idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeState(CharacterStates.Walking);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeState(CharacterStates.Running);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeState(CharacterStates.Idle);
        }
    }

    private void ChangeState(CharacterStates state)
    {
        characterState = state;
        switch (state)
        {
            case CharacterStates.Idle:
                animatorHandler.PlayAnimation("Idle");
                break;
            case CharacterStates.Walking:
                animatorHandler.PlayAnimation("Walk");
                break;
            case CharacterStates.Running:
                animatorHandler.PlayAnimation("Running");
                break;
        }
        
    }
}

public enum CharacterStates
{
    Idle,
    Walking,
    Running,
}