using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 味付けができるスクリプトをつける際に必要なインタフェース
/// </summary>
public interface IRicecakeFlavoring
{
    RicecakeType MyType { get; }
}
