using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅の大きさを変更
/// </summary>
public class RicecakeChangeScale : MonoBehaviour
{
    private float sizeMagAdjustment = 0.7f;
    
    //サイズをくっつける
    public void StickSize(Collision _otherRicecake)
    {
        Vector3 _otherRicecakePos = _otherRicecake.transform.position;
        Vector3 _otherRicecakeScale = _otherRicecake.transform.localScale;

        ChooseMergeRicecake(_otherRicecake);

        //ここから下は選ばれた片方のみ実行

        transform.position = (transform.position + _otherRicecakePos) / 2;

        SoundManager.Instance.PlaySE(SESource.riceCakeUnion);

        gameObject.transform.localScale += _otherRicecakeScale;
        gameObject.transform.localScale *= sizeMagAdjustment;
    }

    //基となる餅を選び、選ばなかった方は消す
    private void ChooseMergeRicecake(Collision _otherRicecake)
    {
        if (IsLargeObjectID(_otherRicecake))
        {
            gameObject.SetActive(false);
            return;
        }
    }

    //インスタンスIDが大きい方を消す
    private bool IsLargeObjectID(Collision _otherRicecake)
    {
        return gameObject.GetInstanceID() > _otherRicecake.gameObject.GetInstanceID();
    }

    //サイズを小さくする
    public void CutSize()
    {
        gameObject.transform.localScale *= sizeMagAdjustment;
    }
}
