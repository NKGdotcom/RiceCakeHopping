using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの調整パラメータ
/// </summary>
public class HoppingData : ScriptableObject
{
    [Header("傾けるスピード")]
    public float RotationSpeed;
    [Header("バウンドする力")]
    public float BouncePower;
    [Header("なめらかな回転")]
    public float SmoothRotation;
}
