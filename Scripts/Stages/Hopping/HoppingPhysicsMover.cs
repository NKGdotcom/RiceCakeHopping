using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの物理関連の挙動
/// </summary>
public class HoppingPhysicsMover : MonoBehaviour
{
    //コンポーネント参照
    private Rigidbody hoppingRb;

    void Awake()
    {
        TryGetComponent<Rigidbody>(out hoppingRb);
    }

    /// <summary>
    /// 物理挙動を用いたジャンプ挙動
    /// </summary>
    /// <param name="_jumpDirection"></param>
    public void Jump(Vector3 _jumpDirection)
    {
        hoppingRb.velocity = _jumpDirection;
    }
}
