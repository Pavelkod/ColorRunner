using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int _maxLevelTilesLength = 20;
    [SerializeField] int _minLevelTilesLength = 10;
    [SerializeField] int _maxSections = 3;
    [SerializeField] int _minSections = 1;
    [SerializeField] float _sectionStep = 3.5f;
    [SerializeField] float _step = 4;
    [SerializeField] float _sectionWidth = 4f;
    [SerializeField] float _groundHeight = .4f;
    [SerializeField, Header("Prefabs")] Transform _pfStart;
    [SerializeField] Transform _pfEnd;
    [SerializeField] Transform _pfSectionSplit;
    [SerializeField] Transform _pfPath;

    [Header("Collectables Settings")]
    [SerializeField] Transform _pfCollectable;

    [SerializeField] GameColors _colors;
    [SerializeField] float _heightAboveGround = 1;
    [SerializeField] float _segmentMargin = 2f;
    [SerializeField] Transform _collectablesParent;
    [SerializeField, Header("Color Zone")] ColorZone _pfColorZone;

    private float _nextPointer = 0;
    private float _nextCollectableRowPointer = 0;
    private int[] _collectablesRow;
    private ICollectablesSpawner[] _collectableSpawners;

    private void Start()
    {
        _collectableSpawners = GetComponents<ICollectablesSpawner>().Where(x => x.IsActive).ToArray();
        MakeLevel();
        GameManager.Instance.OnBeforeStateChange += OnStateChange;
    }

    private void OnStateChange(States state)
    {
        if (state != States.Game) return;
        DeleteLevel();
        MakeLevel();
    }

    private void DeleteLevel()
    {
        foreach (Transform obj in transform)
        {
            Destroy(obj.gameObject);
        }

        foreach (Transform obj in _collectablesParent)
        {
            Destroy(obj.gameObject);
        }
    }

    public void MakeLevel()
    {
        var levelLength = Random.Range(_minLevelTilesLength, _maxLevelTilesLength + 1);
        var sectionCount = Random.Range(_minSections, _maxSections + 1);
        var sectionLength = levelLength / sectionCount;
        var currentZoneColor = 0;//Random.Range(0, _colors.Colors.Count);
        _nextPointer = 0;

        CollectiblesSegmentParams segmentParams = new CollectiblesSegmentParams
        {
            Prefab = _pfCollectable,
            Parent = _collectablesParent,
            MaxColorIndex = _colors.Colors.Count - 1,
            Width = _sectionWidth,
        };

        for (int s = 0; s < sectionCount; s++)
        {
            if (s == 0)
            {
                InstantiateTile(_pfStart);
            }
            else
            {
                InstantiateTile(_pfSectionSplit);
                currentZoneColor = Hlp.ClampIndex(++currentZoneColor, _colors.Colors.Count);
                InstantiateColorZone(currentZoneColor);
            }

            _nextPointer += _sectionStep;

            segmentParams.Start = _nextPointer + _segmentMargin;
            segmentParams.End = _nextPointer + sectionLength * _step - _segmentMargin;
            segmentParams.ColorIndex = currentZoneColor;

            _collectableSpawners[Random.Range(0, _collectableSpawners.Length)].Spawn(segmentParams);

            for (int i = 0; i < sectionLength; i++)
            {
                InstantiateTile(_pfPath);
                if (i < sectionLength - 1) _nextPointer += _step;
            }

            _nextPointer += _sectionStep;
        }

        InstantiateTile(_pfEnd);
    }

    private void InstantiateColorZone(int colorIndex)
    {
        var zone = Instantiate(_pfColorZone, new Vector3(0, 0, _nextPointer), Quaternion.identity, _collectablesParent);
        zone.Init(colorIndex);
    }

    private Transform InstantiateTile(Transform prefab)
    {
        var obj = Instantiate(prefab, new Vector3(0, 0, _nextPointer), Quaternion.identity, transform);
        obj.gameObject.layer = transform.gameObject.layer;
        return obj;
    }

}
