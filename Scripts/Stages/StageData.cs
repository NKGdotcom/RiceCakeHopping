using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RicecakeType
{
    Normal, Kinako, Soysource,
}

/// <summary>
/// そのステージでのクリア条件をください
/// </summary>
[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    [Header("クリア条件の餅のタイプ")]
    public RicecakeType TargetRicecakeType;
    [Header("クリア条件の餅の大きさ")]
    public float TargetSize;
    [Header("制限時間")]
    public float TimeLimit = 60f;
    [Header("ゲーム内で書くクリア条件")]
    public string StageDescription;
}
