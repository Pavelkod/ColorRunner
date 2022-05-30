using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] GameObject _WinGameCanvas;
    [SerializeField] GameObject _LoseGameCanvas;
    [SerializeField] GameObject _hudCanvas;
    [SerializeField] GameObject _scoresCanvas;
    private void OnEnable()
    {
        GameManager.Instance.OnAfterStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAfterStateChange -= OnGameStateChange;
    }

    private void OnGameStateChange(States state)
    {

        _mainMenuCanvas.SetActive(state == States.Menu);
        _WinGameCanvas.SetActive(state == States.Win);
        _LoseGameCanvas.SetActive(state == States.Lose);
        _hudCanvas.SetActive(state == States.Game);
        _scoresCanvas.SetActive(state == States.Win || state == States.Lose);
    }
}
