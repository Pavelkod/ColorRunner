using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Hud : MonoBehaviour
{
    [SerializeField] PlayerPower _playerPower;
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _powerText;
    [SerializeField] float _animationSpeed = 1;

    float _targetValue = 0;

    private void OnEnable()
    {
        _playerPower.OnPowerChange += OnPlayerPowerChange;
    }

    private void OnDisable()
    {
        _playerPower.OnPowerChange -= OnPlayerPowerChange;
        GameManager.Instance.OnBeforeStateChange += ResetHud;
    }

    private void ResetHud(States state)
    {
        if (state != States.Game) return;
        _slider.value = 0;
        _targetValue = 0;
        _powerText.SetText(_targetValue.ToString());
    }
    private void OnPlayerPowerChange(int power)
    {
        _powerText.SetText(power.ToString());
        _targetValue = power;
    }

    private void Update()
    {
        _slider.value = Mathf.Lerp(_slider.value, _targetValue, Time.deltaTime * _animationSpeed);
    }


}
