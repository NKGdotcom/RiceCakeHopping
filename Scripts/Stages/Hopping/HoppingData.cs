using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの挙動を調整するパラメータ
/// </summary>
public class HoppingData : ScriptableObject
{
    [Header("回転・傾き設定")]
    [Tooltip("ホッピングが傾くスピード")]
    public float RotationSpeed;
    [Tooltip("回転時の動きの滑らかさ")]
    public float SmoothRotation;

    [Header("ジャンプ設定")]
    [Tooltip("地面にぶつかった際に跳ね返る力の強さ")]
    public float BouncePower;
}
