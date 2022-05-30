using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

public class CollectableBezierSpawner : CollectiblesRandomLinesCount, ICollectablesSpawner
{
    [SerializeField, Header("Bezier Specific")]
    float _spawnPerUnit = 1f;
    private float _segmentStart;
    private float _segmentEnd;

    public override void Spawn(CollectiblesSegmentParams segmentParams)
    {
        _segmentStart = segmentParams.Start;
        _segmentEnd = segmentParams.End;
        base.Spawn(segmentParams);
    }
    protected override void OnAfterSegmentSpawn()
    {
        float startShift = UnityEngine.Random.value > .5 ? -_width / 2 : _width / 2;
        float endShift = -startShift;
        float segmentMiddle = _segmentStart + (_segmentEnd - _segmentStart) / 2;

        Vector3 p0 = new Vector3(startShift, _heightAboveGround, _segmentStart);
        Vector3 p1 = new Vector3(startShift, _heightAboveGround, segmentMiddle);
        Vector3 p2 = new Vector3(endShift, _heightAboveGround, segmentMiddle);
        Vector3 p3 = new Vector3(endShift, _heightAboveGround, _segmentEnd);

        Debug.DrawLine(p0, p1, Color.green, 100);
        Debug.DrawLine(p0, p1, Color.green, 100);
        Debug.DrawLine(p1, p2, Color.green, 100);
        Debug.DrawLine(p2, p3, Color.green, 100);

        BezierCubic3D bezier = new BezierCubic3D(p0, p1, p2, p3);

        RemoveCollectablesAroundPath();
        SpawnCollectablesAlongPath();

        void RemoveCollectablesAroundPath()
        {
            float timeStep = .01f;
            float time = 0f;
            float castRadius = .5f;
            LayerMask layerMask = 1 << _collectablesParent.gameObject.layer;
            Vector3 prevPoint = Vector3.zero;

            while (time < 1)
            {
                var point = bezier.GetPoint(time);
                var hits = Physics.OverlapSphere(point, castRadius, layerMask);
                if (time > 0)
                    Debug.DrawLine(prevPoint, point, Color.red, 100);
                prevPoint = point;
                foreach (var hit in hits)
                {
                    Destroy(hit.gameObject);
                }
                time = Mathf.MoveTowards(time, 1, timeStep);
            }
        }

        void SpawnCollectablesAlongPath()
        {
            float segmentLength = _segmentEnd - _segmentStart;
            float timeStep = 1 / (segmentLength * _spawnPerUnit);
            float time = 0;
            while (time < 1)
            {
                InstantiateAtPos(bezier.GetPoint(time), _segmentColorIndex);
                time = Mathf.MoveTowards(time, 1, timeStep);
                
            }
        }
    }


}
