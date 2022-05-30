using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAnimation : MonoBehaviour
{
    [SerializeField] float _amplitute = 1f;
    [SerializeField] float _speed;
    Vector3 _position;
    float _shift;

    private void Awake()
    {
        _position = transform.position;
        _shift = _position.y;
    }

    private void Update()
    {
        _position.y = _shift + Mathf.Sin(_position.x + _position.z + Time.time * _speed) * _amplitute;
        transform.position = _position;
    }

}
