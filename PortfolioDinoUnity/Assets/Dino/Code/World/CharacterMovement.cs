using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [TabGroup("States")] [SerializeField] private CharacterStates characterState;
    [TabGroup("States")] [SerializeField] private AnimatorHandler animatorHandler;
    [TabGroup("States")] [SerializeField] private float idleTime = 2f;
    
    [TabGroup("Movement")] [SerializeField] private Transform[] targetsPositions;
    
    [TabGroup("Physics")] [SerializeField] private float speed = 1.5f;
    [TabGroup("Physics")] [SerializeField] private Rigidbody rb;
    
    private bool _characterReachedTarget = false;
    private Queue<Transform> _targetsQueue = new Queue<Transform>();
    private Transform _currentTarget;
    
    private float _rotationSpeed = 3f;
    

    #region Unity Methods
    void Start()
    {
        PopulateQueue();
        ChangeState(CharacterStates.Idle);
    }

    private void Update()
    {
        if (!_characterReachedTarget)
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        if (_currentTarget == null) return;
        if (Vector3.Distance(transform.position, _currentTarget.position) > 0.1f)
        { 
            MoveCharacter();
        }
        else
        {
            // Debug.Log("Character reached target");
            _characterReachedTarget = true;
            
        }
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
        Debug.Log("Current Target = ".SetColor(ColorString.Purple) + _currentTarget.gameObject.name);
    }
    private void MoveCharacter()
    {
        Debug.Log("Character is moving");
        rb.MovePosition(Vector3.MoveTowards(transform.position, _currentTarget.position, speed * Time.deltaTime));
    }
    private void RotateCharacter()
    {
        // Vector3 direction = (_currentTarget.position - transform.position).normalized;
        // Quaternion lookRotation = Quaternion.LookRotation(direction);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        if(_currentTarget == null) return;
        Debug.Log("Character is rotating".SetColor(ColorString.Blue));
        transform.DORotate( _currentTarget.position - transform.position, _rotationSpeed);
    }
    private void ChangeState(CharacterStates state)
    {
        if (characterState == state) return;
        characterState = state;
        Debug.Log("State = ".SetColor(ColorString.Orange) + characterState);
        switch (state)
        {
            case CharacterStates.Idle:
                animatorHandler.PlayAnimation("idle");
                StartCoroutine("IdleCounter");
                break;
            case CharacterStates.Walking:
                UpdateCurrentTarget(); 
                RotateCharacter();
                MoveCharacter(); 
                animatorHandler.PlayAnimation("walk");
                break;
        }
        
    }

    private IEnumerator IdleCounter()
    {
        yield return new WaitForSeconds(idleTime);
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