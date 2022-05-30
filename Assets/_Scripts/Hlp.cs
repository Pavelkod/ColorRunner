using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Hlp
{
    private static Camera _camera;
    public static Camera Cam
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    public static int ClampIndex(int current, int maxIndex) => current % maxIndex;

    public static Color Opaque(this Color color, float amount = 1f) => new Color(color.r, color.g, color.b, amount);

}
