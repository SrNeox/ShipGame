using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomNumber
{
    public static int Create(float minValue, float maxValue)
    {
        float number = Random.Range(minValue, maxValue);
        return (int)number;
    }
}
