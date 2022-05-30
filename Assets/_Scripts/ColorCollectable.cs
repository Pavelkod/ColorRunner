using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Collider))]
public class ColorCollectable : MonoBehaviour, IColorCollectable
{
    [SerializeField] GameColors _gameColors;
    [SerializeField] int _power;
    MeshRenderer _renderer;
    int _colorIndex;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
    }

    public void Collect(Transform transform)
    {
        if (transform.TryGetComponent<IHavePower>(out IHavePower power))
        {
            power.TakePower(_colorIndex, _power);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collect(other.transform);
    }

    public void Init(int colorIndex)
    {
        _colorIndex = colorIndex;
        _renderer.material.color = _gameColors.Colors[colorIndex];
    }
}
