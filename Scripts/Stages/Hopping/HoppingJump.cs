using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングのジャンプの挙動
/// </summary>
public class HoppingJump : MonoBehaviour
{
    [SerializeField] private HoppingPhysicsMover hoppingPhysicsMover;

    private float bouncePower;
    private void Awake()
    {
        if (hoppingPhysicsMover == null) { Debug.LogError("hoppingPhysicsMoverが参照されていません"); return; }
    }

    public void SetUp(HoppingData _data)
    {
        bouncePower = _data.BouncePower;
    }

    //ホッピングのジャンプの方向を再現
    //ホッピングのジャンプはまた別のスクリプトで実行
    public void HoppingJumpMovement()
    {
        SoundManager.Instance.PlaySE(SESource.hoppingMove);
        Vector3 _jumpDirection = transform.up;
        hoppingPhysicsMover.Jump(_jumpDirection * bouncePower);
    }
}
