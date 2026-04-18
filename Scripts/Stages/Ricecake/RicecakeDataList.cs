using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅の味ごとに色と出現させるパーティクルを設定
/// </summary>
[CreateAssetMenu(fileName = "RicecakeData", menuName = "ScriptableObjects/RicecakeData", order = 1)]
public class RicecakeDataList : ScriptableObject
{
    [Header("味付けデータ構造")]
    [SerializeField] private List<RicecakeData> ricecakeDataList;
    public List<RicecakeData> DataList { get => ricecakeDataList; private set => ricecakeDataList = value; }
}

/// <summary>
/// 餅の味付けごとの色とエフェクトのデータ
/// </summary>
[System.Serializable]
public class RicecakeData
{
    [Header("味付けのパラメータ")]
    [Tooltip("餅の味付けを構造体で設定")]
    public RicecakeType RicecakeType;
    [Tooltip("味付けの色")]
    public Material RicecakeMaterial;
    [Tooltip("餅に触れたときに発火させるパーティクル")]
    public GameObject HitEffectPrefab;
}
