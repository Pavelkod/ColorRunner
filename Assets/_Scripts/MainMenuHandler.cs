using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour, IPreAwake
{
    [SerializeField] States[] _showOnState;
    private Animator _animator;
    private int _closeMenuHash = Animator.StringToHash("FadeOut");

    public void PreAwake()
    {
        GameManager.Instance.OnAfterStateChange += OnGameStateChange;
        _animator = GetComponent<Animator>();
    }

    private void OnGameStateChange(States state)
    {
        gameObject.SetActive(_showOnState.Contains(state));
    }

    public void OnCloseMenu()
    {
        _animator.Play(_closeMenuHash);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnBeforeStateChange -= OnGameStateChange;
    }
}
