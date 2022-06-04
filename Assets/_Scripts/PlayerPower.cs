using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour, IHavePower, IColorTaker
{
    [SerializeField] GameColors _gameColors;
    [SerializeField] int _maxPower = 10;
    [SerializeField] float _powerScaleStep = .07f;
    [SerializeField] float _growSpeed = 1;
    [SerializeField] ScoresContainer _soScoresContainer;
    [SerializeField, Header("Game Events")] GameEvent _powerUpEvent;
    [SerializeField] GameEvent _powerDownEvent;
    [SerializeField] GameEvent _changeColorEvent;

    private bool _isActive = false;

    public event Action<int> OnPowerChange;

    SkinnedMeshRenderer _renderer;
    private int _currentColorIndex = 0;
    private int _currentPower;

    Vector3 _targetScale = Vector3.one;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _renderer.sharedMaterial.color = _gameColors.Colors[_currentColorIndex];
    }

    private void OnEnable()
    {
        GameManager.Instance.OnBeforeStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(States state)
    {
        _isActive = States.Game == state;
        _soScoresContainer.Score = _currentPower;

        if (state != States.Game) return;

        ResetPower();
    }

    private void ResetPower()
    {
        _soScoresContainer.Score = 0;
        _currentPower = 0;
        SetColor(0);
        UpdatePower();
    }

    void IHavePower.TakePower(int colorIndex, int amount)
    {
        if (!_isActive) return;
        if (colorIndex == _currentColorIndex)
        {
            _currentPower += amount;
            _powerUpEvent.FireEvent();
        }
        else
        {
            _currentPower -= amount;
            _powerDownEvent.FireEvent();
            if (_currentPower < 0) GameManager.Instance.SetState(States.Lose);
        }

        _currentPower = Mathf.Clamp(_currentPower, 0, _maxPower);
        UpdatePower();
    }

    private void UpdatePower()
    {
        OnPowerChange?.Invoke(_currentPower);
        _targetScale = Vector3.one * (1 + _currentPower * _powerScaleStep);
    }

    void IColorTaker.TakeColor(int colorIndex)
    {
        SetColor(colorIndex);
        _changeColorEvent.FireEvent();
    }

    private void SetColor(int colorIndex)
    {
        _currentColorIndex = colorIndex;
        _renderer.material.color = _gameColors.Colors[colorIndex];
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetScale, Time.deltaTime * _growSpeed);
    }


}

