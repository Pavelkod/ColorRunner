using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetect : MonoBehaviour, ISwipeProvider
{
    [SerializeField] Animator _animator;
    [SerializeField] PlayerInput _playerInput;

    [SerializeField] float _swipeDistanceThreshold = 10f;
    [SerializeField] float _maxSwipeTime = 1f;
    [SerializeField] float _dirThreshold = .8f;
    [SerializeField] float _normalizeInput = 10f;

    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private Vector2 _endPosition;

    private float _startTime;

    #region Events
    public event Action<float> OnHorizontalSwipe;
    public event Action<float> OnVerticalSwipe;

    #endregion

    private void Awake()
    {
        _playerInput.actions["Touch"].started += OnStartTouch;
        _playerInput.actions["TouchPosition"].performed += OnTouchPositionUpdate;
    }

    private void OnTouchPositionUpdate(InputAction.CallbackContext ctx)
    {
        _currentPosition = ctx.ReadValue<Vector2>();
        SwipeHandler((_currentPosition - _startPosition).normalized);
    }

    private void OnDestroy()
    {
        _playerInput.actions["Touch"].started -= OnStartTouch;
        _playerInput.actions["TouchPosition"].performed -= OnTouchPositionUpdate;
    }

    private void OnStartTouch(InputAction.CallbackContext obj)
    {
        _startTime = Time.time;
        _startPosition = Touchscreen.current.position.ReadValue();
    }

    private void OnEndTouch(InputAction.CallbackContext obj)
    {
        var endPosition = Touchscreen.current.position.ReadValue();

        if (Time.time - _startTime < _maxSwipeTime && Vector2.Distance(_startPosition, endPosition) > _swipeDistanceThreshold) SwipeHandler((endPosition - _startPosition).normalized);
    }



    private void SwipeHandler(Vector2 dir)
    {
        var verticalMagnitude = (_currentPosition.y - _startPosition.y) / _normalizeInput;
        var horizontalMagnitude = (_currentPosition.x - _startPosition.x) / _normalizeInput;
        OnVerticalSwipe?.Invoke(NormalizeInput(verticalMagnitude));
        OnHorizontalSwipe?.Invoke(NormalizeInput(horizontalMagnitude));
    }

    private float NormalizeInput(float magnitude)
    {
        return Mathf.Clamp(magnitude / _normalizeInput, -1, 1);
    }
}
