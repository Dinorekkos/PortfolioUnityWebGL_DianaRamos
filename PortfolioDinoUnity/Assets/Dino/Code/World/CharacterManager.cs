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
    // [TabGroup("Movement")] [SerializeField] private int _currentTargetIndex = 0;
    
    [TabGroup("Physics")] [SerializeField] private float speed = 1f;
    [TabGroup("Physics")] [SerializeField] private Rigidbody rb;
    
    private bool _isMoving = false;
    private bool _characterIsRotating = false;
    private bool _characterReachedTarget = false;
    private Queue<Transform> _targetsQueue = new Queue<Transform>();
    private Transform _currentTarget;
    
    private float _idleCounter = 0f;
    private float _rotationSpeed = 3f;
    

    #region Unity Methods
    void Start()
    {
        PopulateQueue();
        //Start player walking to the first target
        ChangeState(CharacterStates.Idle);
    }

    private void Update()
    {
        if (_isMoving) MoveCharacter();
        
        
       
        
    }
    #endregion

    private void PopulateQueue()
    {
        foreach (var target in targetsPositions)
        {
            _targetsQueue.Enqueue(target);
        }
    }
    
    private Transform GetNextTarget()
    {
        return _targetsQueue.Dequeue();
    }
    private void UpdateCurrentTarget()
    {
        if (_targetsQueue.Count == 0)
        {
            _targetsQueue.Clear();
            PopulateQueue();
        }
        _currentTarget = GetNextTarget();
    }
    private void MoveCharacter()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, _currentTarget.position, speed * Time.deltaTime));
    }
    private void RotateCharacter()
    {
        // Vector3 direction = (_currentTarget.position - transform.position).normalized;
        // Quaternion lookRotation = Quaternion.LookRotation(direction);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        
        transform.DORotate( _currentTarget.position - transform.position, _rotationSpeed);
    }
    private void ChangeState(CharacterStates state)
    {
        characterState = state;
        switch (state)
        {
            case CharacterStates.Idle:
                _idleCounter = idleTime;
                animatorHandler.PlayAnimation("idle");
                StartCoroutine("IdleCounter");
                break;
            case CharacterStates.Walking:
                _isMoving = true;
                animatorHandler.PlayAnimation("walk");
                break;
        }
    }

    private IEnumerator IdleCounter()
    {
        while (_idleCounter > 0)
        {
            _idleCounter -= Time.deltaTime;
            yield return null;
        }
        UpdateCurrentTarget();
        ChangeState(CharacterStates.Walking);
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