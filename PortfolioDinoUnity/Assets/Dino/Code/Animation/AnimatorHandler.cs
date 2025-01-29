using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorHandler : MonoBehaviour
{
    private Animator _animator;
    public UnityEvent OnStart;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
    }

    private void Start()
    {
        OnStart?.Invoke();
    }

    public void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }
    
    public void SetBool(string boolName, bool value)
    {
        _animator.SetBool(boolName, value);
    }
    
    public void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
    
    public void SetAnimationLoop(string animationName)
    {
        
    }
}
