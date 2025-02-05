using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [TabGroup("States")] [SerializeField] private CharacterStates characterState;
    [TabGroup("States")] [SerializeField] private AnimatorHandler animatorHandler;
    [TabGroup("States")] [SerializeField] private float idleTime = 3f;
    
    [TabGroup("Movement")] [SerializeField] private Transform[] targetsPositions;
    [TabGroup("Movement")] [SerializeField] private int _currentTargetIndex = 0;
    
    [TabGroup("Physics")] [SerializeField] private float speed = 1f;
    [TabGroup("Physics")] [SerializeField] private Rigidbody rb;
    
    private bool _isMoving = false;
    private bool _characterIsRotating = false;
    private bool _characterReachedTarget = false;
    private float _idleCounter = 0f;

    #region Unity Methods
    void Start()
    {
        //Start player walking to the first target
        SetPlayerInitialPosition();
        ChangeState(CharacterStates.Walking);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetsPositions[_currentTargetIndex].position) < 0.1f)
        {
            _isMoving = false;
            ChangeState(CharacterStates.Idle);
            UpdateCurrentTargetIndex();
        }
        
        if (characterState == CharacterStates.Idle)
        {
            _idleCounter -= Time.deltaTime;
            if (_idleCounter <= 0)
            {
                RotateCharacter();
                ChangeState(CharacterStates.Walking);
            }
        }
        
        if (_isMoving) MoveCharacter();
        
    }
    #endregion

    private void UpdateCurrentTargetIndex()
    {
        _currentTargetIndex++;
        if (_currentTargetIndex >= targetsPositions.Length)
        {
            _currentTargetIndex = 0;
        }
    }
    private void SetPlayerInitialPosition() => transform.position = targetsPositions[0].position;
    private void MoveCharacter()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, targetsPositions[_currentTargetIndex].position, speed * Time.deltaTime));
    }
    private void RotateCharacter()
    {
        Vector3 direction = (targetsPositions[_currentTargetIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void ChangeState(CharacterStates state)
    {
        characterState = state;
        switch (state)
        {
            case CharacterStates.Idle:
                _idleCounter = idleTime;
                animatorHandler.PlayAnimation("idle");
                break;
            case CharacterStates.Walking:
                _isMoving = true;
                animatorHandler.PlayAnimation("walk");
                break;
        }
    }



    #region Testing
    
    [Button]
    public void ChangeStateToWalking()
    {
        ChangeState(CharacterStates.Walking);
    }
    
    [Button]
    public void ChangeStateToIdle()
    {
        ChangeState(CharacterStates.Idle);
    }
    #endregion

}

public enum CharacterStates
{
    None,
    Idle,
    Walking,
}