using UnityEngine;

public interface ICollectablesSpawner
{
    bool IsActive { get; }
    void Spawn(CollectiblesSegmentParams segmentParams);
}
