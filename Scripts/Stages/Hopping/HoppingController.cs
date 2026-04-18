using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの挙動を統括するコントローラー
/// </summary>
public class HoppingController : MonoBehaviour
{
    [Header("データ")]
    [Tooltip("ホッピングの挙動パラメータ")]
    [SerializeField] private HoppingData hoppingData;

    [Header("各機能の挙動")]
    [Tooltip("ホッピングの移動挙動")]
    [SerializeField] private HoppingMovement hoppingMovement;
    [Tooltip("ホッピングのジャンプ挙動")]
    [SerializeField] private HoppingJump hoppingJump;
    [Tooltip("ホッピングの足元の影")]
    [SerializeField] private HoppingShadowFeet hoppingShadowFeet;

    // Start is called before the first frame update
    void Awake()
    {
        if (hoppingData == null) { Debug.LogError("hoppingDataが参照されていません"); return; }
        if (hoppingMovement == null) { Debug.LogError("hoppingMovementが参照されていません"); return; }
        if (hoppingJump == null) { Debug.LogError("hoppingJumpが参照されていません"); return; }
        if(hoppingShadowFeet == null) { Debug.LogError("hoppingShadowFeetが参照されていません"); return; }
        
        //データを渡してパラメータなどを引き渡す
        hoppingMovement.SetUp(hoppingData);
        hoppingJump.SetUp(hoppingData);
    }

    void Update()
    {
        //毎フレーム移動・傾き
        hoppingMovement.HoppingMoveTilt();

        //影の更新
        hoppingShadowFeet.UpdateShadow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //何かにぶつかったらジャンプ処理
        hoppingJump.HoppingJumpMovement();

        if (collision.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            //餅に触れたら吹き飛ばす
            _ricecake.OnHitByHopping(collision);
            SoundManager.Instance.PlaySE(SESource.RICE_CAKE_COLLISION);
        }
    }
}
