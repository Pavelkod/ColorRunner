using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreWarm : MonoBehaviour
{
    private void Awake()
    {
        var prewarmArray = GetComponentsInChildren<IPreAwake>(true);
        foreach (var item in prewarmArray)
        {
            item.PreAwake();
        }
    }
}
