
using UnityEngine;

/// <summary>
/// 餅のインターフェース
/// </summary>
public interface IRiceCake
{
    RicecakeType MyType { get; }
    float RicecakeSize { get; }
    void MargeRicecake(Collision _collision);
    void StopRicecake();
    void CutRicecake();
    void OnHitByHopping(Collision _collision);
    void OnHitByReflect(Vector3 reflectPos);
}
