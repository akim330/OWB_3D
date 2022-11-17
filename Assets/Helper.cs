using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Helper
{
    public static void PrintArray(float[] arr)
    {
        Debug.Log("Array = [" + String.Join(",",
            new List<float>(arr)
            .ConvertAll(i => i.ToString())
            .ToArray()) + "]");
    }

    public static void PrintArray(Vector3[] arr)
    {
        Debug.Log("Array = [" + String.Join(",",
            new List<Vector3>(arr)
            .ConvertAll(i => i.ToString())
            .ToArray()) + "]");
    }

    public static string ArrayToString(Vector3[] arr)
    {
        return "Array = [" + String.Join(",",
            new List<Vector3>(arr)
            .ConvertAll(i => i.ToString())
            .ToArray()) + "]";
    }
}
