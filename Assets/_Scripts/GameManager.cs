using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action OnGameOver;
    public event Action<States> OnBeforeStateChange;
    public event Action<States> OnAfterStateChange;

    private States _currentState = (States)(-1);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        Init();
    }
    void Init()
    {
        SetState(States.Menu);
    }

    public void SetState(States newState)
    {
        if (newState == _currentState) return;

        _currentState = newState;

        Debug.Log(newState);

        OnBeforeStateChange?.Invoke(newState);

        switch (newState)
        {
            case States.Menu:
                break;
            case States.Game:
                break;
            case States.Lose:
                break;
            case States.Win:
                break;
        }

        OnAfterStateChange?.Invoke(newState);
    }
}

public enum States
{
    Menu,
    Game,
    Win,
    Lose,
}
