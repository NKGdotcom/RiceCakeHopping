
using UnityEngine;

/// <summary>
/// 餅の定義を持つインタフェース
/// </summary>
public interface IRiceCake
{
    /// <summary>
    /// 餅の味付け
    /// </summary>
    RicecakeType MyType { get; }

    /// <summary>
    /// 餅のサイズ
    /// </summary>
    float RicecakeSize { get; }

    /// <summary>
    /// 餅の合体
    /// </summary>
    /// <param name="_collision"></param>
    void MargeRicecake(Collision _collision);

    /// <summary>
    /// 餅を強制停止
    /// </summary>
    void StopRicecake();

    /// <summary>
    /// 餅をカットし、小さくする
    /// </summary>
    void CutRicecake();

    /// <summary>
    /// ホッピングに触れた場合
    /// </summary>
    /// <param name="_collision"></param>
    void OnHitByHopping(Collision _collision);

    /// <summary>
    /// 反射板のようなものに触れた場合
    /// </summary>
    /// <param name="reflectPos"></param>
    void OnHitByReflect(Vector3 reflectPos);
}
