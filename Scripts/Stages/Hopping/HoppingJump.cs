using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床に触れたらホッピングがジャンプする挙動
/// </summary>
public class HoppingJump : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("物理挙動を担当するクラス")]
    [SerializeField] private HoppingPhysicsMover hoppingPhysicsMover;

    [Header("パラメータ設定")]
    [Tooltip("地面に触れたときの跳ね返りの強さ(デバッグ用)")]
    [SerializeField] private float bouncePower;

    private void Awake()
    {
        if (hoppingPhysicsMover == null) { Debug.LogError("hoppingPhysicsMoverが参照されていません"); return; }
    }

    /// <summary>
    /// 初期データのパラメータ設定
    /// </summary>
    /// <param name="_data"></param>
    public void SetUp(HoppingData _data)
    {
        bouncePower = _data.BouncePower;
    }

    /// <summary>
    /// ホッピングのジャンプ処理を実行する
    /// </summary>
    public void HoppingJumpMovement()
    {
        SoundManager.Instance.PlaySE(SESource.HOPPING_MOVE);

        //ホッピングの上方向に力を加える
        Vector3 _jumpDirection = transform.up;

        //物理挙動クラスでジャンプ
        hoppingPhysicsMover.Jump(_jumpDirection * bouncePower);
    }
}
