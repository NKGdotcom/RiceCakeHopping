using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅の味ごとに色とパーティクルを設定
/// </summary>
[CreateAssetMenu(fileName = "RicecakeData", menuName = "ScriptableObjects/RicecakeData", order = 1)]
public class RicecakeDataList : ScriptableObject
{
    [SerializeField] private List<RicecakeData> ricecakeDataList;
    public List<RicecakeData> DataList { get => ricecakeDataList; private set => ricecakeDataList = value; }
}
[System.Serializable]
public class RicecakeData
{
    [Header("餅の味")]
    public RicecakeType RicecakeType;
    [Header("餅の色")]
    public Material RicecakeMaterial;
    [Header("餅のパーティクル")]
    public GameObject HitEffectPrefab;
}
