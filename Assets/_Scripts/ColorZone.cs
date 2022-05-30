using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorZone : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particleSystems;
    [SerializeField] private GameColors _colors;

    public int ColorIndex { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IColorTaker>(out IColorTaker ct)) return;
        ct.TakeColor(ColorIndex);
    }

    public void Init(int colorIndex)
    {
        ColorIndex = colorIndex;
        Color color = _colors.Colors[colorIndex];
        var startColor = new ParticleSystem.MinMaxGradient((color / 1.5f).Opaque(), color);

        foreach (var ps in _particleSystems)
        {
            var main = ps.main;
            main.startColor = startColor;
        }


    }
}
