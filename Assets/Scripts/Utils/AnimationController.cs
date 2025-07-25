﻿using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Callbacks
    public Action<string> OnAnimationStarted;
    public Action<string> OnAnimationCompleted;
    public Action<string, float> OnAnimationUpdated; // Animation name and normalized time

    private string _currentAnimation;
    private bool _isAnimationPlaying;
    private GameObject _gameObject;

    private void Awake()
    {
        if (animator == null)
            animator = gameObject.GetComponent<Animator>();

        _gameObject = animator.gameObject;
    }

    private void Update()
    {
        if (animator == null || animator.runtimeAnimatorController == null || !_gameObject.activeInHierarchy) return;

        // Get current state information
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Check if an animation is playing
        if (stateInfo.length > 0 && stateInfo.normalizedTime < 1f)
        {
            string animationName = stateInfo.shortNameHash.ToString();

            // Trigger start callback when animation begins
            if (!_isAnimationPlaying || _currentAnimation != animationName)
            {
                _currentAnimation = animationName;
                _isAnimationPlaying = true;
                OnAnimationStarted?.Invoke(animationName);
            }

            // Trigger update callback each frame
            OnAnimationUpdated?.Invoke(_currentAnimation, stateInfo.normalizedTime);
        }
        else if (_isAnimationPlaying)
        {
            // Trigger complete callback when animation ends
            OnAnimationCompleted?.Invoke(_currentAnimation);
            _isAnimationPlaying = false;
            _currentAnimation = null;
            this.enabled = false;
        }
    }

    public void PlayAnimation(string animationName)
    {
        if (animator == null) return;

        this.enabled = true;
        animator.Play(animationName);
    }

    public void PlayAnimation(string animationName, Action<string> onAnimationCompleteCallback)
    {
        if (animator == null) return;

        this.enabled = true;
        OnAnimationCompleted = onAnimationCompleteCallback;
        animator.Play(animationName);
    }

    public void SetAnimator(Animator targetAnimator)
    {
        animator = targetAnimator;
    }
}
