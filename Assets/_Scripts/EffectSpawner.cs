using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Perform(Vector3 point);
}

public class EffectSpawner : MonoBehaviour, IEffect
{
    [SerializeField] Transform _pfEffect;
    [SerializeField] float _timeOut = 5;
    public void Perform(Vector3 point)
    {
        var obj = Instantiate(_pfEffect, point, Quaternion.identity);
        Destroy(obj.gameObject, _timeOut);
    }
}
