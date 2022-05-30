using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesLineSpawner : MonoBehaviour, ICollectablesSpawner
{
    public bool IsActive => enabled;

    [SerializeField] protected float _heightAboveGround;
    [SerializeField] protected int _countInRow = 3;
    [SerializeField] protected float _zSpread = 1f;
    protected float _width;
    private float _nextCollectableRowPointer;
    Transform _pfCollectable;
    protected int[] _collectablesRow;

    private int _maxColorIndex;
    protected int _segmentColorIndex;
    protected Transform _collectablesParent;


    private void Start()
    {
        _collectablesRow = new int[_countInRow];
    }

    public virtual void Spawn(CollectiblesSegmentParams segmentParams)
    {
        _pfCollectable = segmentParams.Prefab;
        _collectablesParent = segmentParams.Parent;
        _width = segmentParams.Width;
        _maxColorIndex = segmentParams.MaxColorIndex;
        _segmentColorIndex = segmentParams.ColorIndex;

        InstantiateColorCollectablesSegment(segmentParams.Start, segmentParams.End, 0);

    }

    private void InstantiateColorCollectablesSegment(float segmentStart, float segmentEnd, int segmentColor)
    {
        float startShift = _width / 2;
        int count = Mathf.Max(_collectablesRow.Length, 2);
        float step = _width / (count - 1);
        _nextCollectableRowPointer = segmentStart;

        OnBeforeSegmentSpawn();
        while (_nextCollectableRowPointer < (segmentEnd))
        {
            OnBeforeRowSpawn();
            for (int i = 0; i < _collectablesRow.Length; i++)
            {
                InstantiateAtShift(_collectablesRow[i], i * step - startShift);
            }

            _nextCollectableRowPointer += _zSpread;
        }

        OnAfterSegmentSpawn();
    }

    protected virtual void OnBeforeSegmentSpawn()
    {
        MakeColorsRow();
    }

    protected virtual void OnBeforeRowSpawn()
    {

    }

    protected virtual void OnAfterSegmentSpawn()
    {

    }

    private IColorCollectable InstantiateAtShift(int colorIndex, float xShift)
    {

        return InstantiateAtPos(new Vector3(xShift, _heightAboveGround, _nextCollectableRowPointer), colorIndex);
    }

    protected IColorCollectable InstantiateAtPos(Vector3 position, int colorIndex)
    {
        var collectableTransform = Instantiate(_pfCollectable, position, Quaternion.identity, _collectablesParent);
        collectableTransform.gameObject.layer = _collectablesParent.gameObject.layer;
        var collectable = collectableTransform.GetComponent<IColorCollectable>();
        collectable.Init(colorIndex);
        return collectable;
    }

    protected void MakeColorsRow()
    {
        var colorIndex = Random.Range(0, _maxColorIndex + 1);
        var direction = Random.value > .5 ? 1 : -1;
        _collectablesRow[0] = colorIndex;
        for (int i = 1; i < _collectablesRow.Length; i++)
        {
            colorIndex += direction;

            if (colorIndex > _maxColorIndex) colorIndex = 0;
            if (colorIndex < 0) colorIndex = _maxColorIndex;

            _collectablesRow[i] = colorIndex;
        }
    }
}
