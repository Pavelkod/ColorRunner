using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] Transform _pfEffect;
    [SerializeField] float _timeOut = 5;
    [SerializeField] Vector3 _spawnPostion;
    public void Perform()
    {
        var obj = Instantiate(_pfEffect, _spawnPostion + transform.position, Quaternion.identity);
        Destroy(obj.gameObject, _timeOut);
    }
}
