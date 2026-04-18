using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お餅を黄な粉味にする機能を持つコンポーネント
/// </summary>
public class SoysauceFlavoring : MonoBehaviour, IRicecakeFlavoring
{
    /// <summary>
    /// このオブジェクトが提供する味付けの種類(醤油味)
    /// </summary>
    public RicecakeType MyType => RicecakeType.Kinako;
}
