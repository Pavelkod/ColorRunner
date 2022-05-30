using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRandomLineSpawner : CollectablesLineSpawner
{
    protected override void OnBeforeSegmentSpawn()
    {
    }

    protected override void OnBeforeRowSpawn()
    {
        MakeColorsRow();
    }
}
