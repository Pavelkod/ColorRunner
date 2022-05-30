using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesRandomLinesCount : CollectablesLineSpawner
{
    [SerializeField, Header("Lines ranomizator")] int _maxRows = 5;
    [SerializeField] int _minRows = 2;

    [SerializeField] float _distanceBetweenLines = 1.5f;

    public override void Spawn(CollectiblesSegmentParams segmentParams)
    {
        _countInRow = Random.Range(_minRows, _maxRows + 1);
        _collectablesRow = new int[_countInRow];
        base.Spawn(segmentParams);
    }
}
