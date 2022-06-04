using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 1f;
    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _maxAngle = 30f;
    [SerializeField, Header("Ground Detection")] private Vector3 _groundDetectOrigin;
    [SerializeField] private float _groundDetectRadius;

    private Vector3 _startPoint;

    int _speedHash = Animator.StringToHash("Speed");

    GameManager _gameManager;
    private float _targetSpeed;

    private Rigidbody _rb;

    private int _currentTrack = 0;
    private float _currentSpeed = 0;

    private Animator _animator;
    private ISwipeProvider _swipeProvider;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _swipeProvider = GetComponent<ISwipeProvider>();
        _rb = GetComponent<Rigidbody>();
        _startPoint = transform.position;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnAfterStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(States state)
    {
        if (States.Game == state)
        {
            _rb.velocity = Vector3.zero;
            _rb.rotation = Quaternion.identity;
            transform.position = _startPoint;
        }
        _targetSpeed = state == States.Game ? _maxSpeed : 0;
    }

    private void OnEnable()
    {
        _swipeProvider.OnHorizontalSwipe += OnChangeDirection;
    }

    private void OnChangeDirection(float input)
    {
        _targetRotation = Quaternion.Euler(0, _maxAngle * input, 0);
    }

    private void OnDisable()
    {
        _swipeProvider.OnHorizontalSwipe -= OnChangeDirection;
    }

    private void Update()
    {
        _rb.rotation = Quaternion.RotateTowards(_rb.rotation, _targetRotation, Time.deltaTime * _rotationSpeed).normalized;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _targetSpeed, Time.deltaTime * _acceleration);
        _animator.SetFloat(_speedHash, _currentSpeed);
        _rb.velocity = transform.forward * _currentSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundDetectOrigin, _groundDetectRadius);
    }

}
