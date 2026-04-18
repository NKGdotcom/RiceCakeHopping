using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅同士やナイフが触れると、サイズ感が変わる
/// </summary>
public class RicecakeChangeScale : MonoBehaviour
{
    //単に合体して大きくすると大きくなりすぎるため少し小さくする調整
    private float sizeMagAdjustment = 0.7f;
    
    /// <summary>
    /// 餅同士をくっつけてサイズを大きくする
    /// </summary>
    /// <param name="_otherRicecake"></param>
    public void StickSize(Collision _otherRicecake)
    {
        //相手方の餅の位置と大きさを保存
        Vector3 _otherRicecakePos = _otherRicecake.transform.position;
        Vector3 _otherRicecakeScale = _otherRicecake.transform.localScale;

        transform.position = (transform.position + _otherRicecakePos) / 2;

        SoundManager.Instance.PlaySE(SESource.RICE_CAKE_UNION);

        //サイズの調整
        gameObject.transform.localScale += _otherRicecakeScale;
        gameObject.transform.localScale *= sizeMagAdjustment;
    }

    /// <summary>
    /// サイズを包丁で小さくする
    /// </summary>
    public void CutSize()
    {
        gameObject.transform.localScale *= sizeMagAdjustment;
    }
}
