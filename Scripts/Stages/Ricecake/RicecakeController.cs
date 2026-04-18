using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お持ちのステータス管理や他ギミックとの衝突判定を統括する
/// </summary>
public class RicecakeController : MonoBehaviour, IRiceCake
{
    [Header("コンポーネント参照")]
    [Tooltip("物理的な移動や、跳ね返りの力を管理するクラス")]
    [SerializeField] private RicecakePhysicsMover physicsMover;
    [Tooltip("合体時やカット時のサイズ変更を管理するクラス")]
    [SerializeField] private RicecakeChangeScale ricecakeChangeScale;
    [Tooltip("味に応じた色や触れたときのエフェクトの変更を管理するクラス")]
    [SerializeField] private RicecakeChangeColor ricecakeChangeColor;
    [Tooltip("ぶつかったときのパーティクル演出を管理するクラス")]
    [SerializeField] private RicecakeHitEffect ricecakeHitEffect;

    //自分の今の味付け
    public RicecakeType MyType { get; private set; }
    //自分の今の大きさ
    public float RicecakeSize { get; private set; } = 1;

    private void Awake()
    {
        if (physicsMover == null) { Debug.LogError("physicsMoverが参照されていません"); return; }
        if (ricecakeChangeScale == null) { Debug.LogError("ricecakeChangeScaleが参照されていません"); return; }
        if (ricecakeChangeColor == null) { Debug.LogError("ricecakeChangeColorが参照されていません"); return; }
        if (ricecakeHitEffect == null) { Debug.LogError("ricecakeHitEffectが参照されていません"); return; }

        RicecakeSize = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            //お互いが同時に吸収しあわないよう、InstanceIDが大きいほうだけが処理を行う
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
            {
                MargeRicecake(collision);
                SoundManager.Instance.PlaySE(SESource.RICE_CAKE_UNION);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //ぶつかってきた味付けギミックを取得
        if (other.gameObject.TryGetComponent<IRicecakeFlavoring>(out var _ricecakeFlavoring))
        {
            other.gameObject.SetActive(false);

            //味付けを変える
            ChangeFlavoring(_ricecakeFlavoring);
            SoundManager.Instance.PlaySE(SESource.RICE_CAKE_UNION);
        }
    }

    /// <summary>
    /// 餅同士がぶつかったときの結合処理
    /// </summary>
    /// <param name="_otherRicecake"></param>
    public void MargeRicecake(Collision _otherRicecake)
    {
        //大きくする
        RicecakeSize++;

        //エフェクトを発火
        ricecakeHitEffect.ShowHitParticle(MyType);

        //餅同士が触れたときにピタッと止める
        StopRicecake();

        //餅をくっつける
        ricecakeChangeScale.StickSize(_otherRicecake);
    }

    /// <summary>
    /// 餅をカットしサイズを小さくする
    /// </summary>
    public void CutRicecake()
    {
        RicecakeSize = RicecakeSize / 2;
        ricecakeHitEffect.ShowHitParticle(MyType);
        ricecakeChangeScale.CutSize();
    }

    /// <summary>
    /// 餅をピタッと止める
    /// </summary>
    public void StopRicecake()
    {
        physicsMover.StopForce();
    }

    /// <summary>
    /// 触れた味を変更する
    /// </summary>
    /// <param name="_ricecake"></param>
    private void ChangeFlavoring(IRicecakeFlavoring _ricecake)
    {
        MyType = _ricecake.MyType;
        StopRicecake();
        ricecakeChangeColor.ChangeRicecakeColor(MyType);
        ricecakeHitEffect.ShowHitParticle(MyType);
    }

    /// <summary>
    /// ホッピングに触れたらエフェクトを出して吹き飛ばされる
    /// </summary>
    /// <param name="_collision"></param>
    public void OnHitByHopping(Collision _collision)
    {
        ricecakeHitEffect.ShowHitParticle(MyType);
        physicsMover.AddForceRicecakeCol(_collision);
    }

    /// <summary>
    /// 反射板に触れたら跳ね返る
    /// </summary>
    /// <param name="_reflectPos"></param>
    public void OnHitByReflect(Vector3 _reflectPos)
    {
        physicsMover.AddForceReflect(_reflectPos);
    }
}
