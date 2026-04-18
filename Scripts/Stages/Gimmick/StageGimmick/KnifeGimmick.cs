using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お餅をカットするナイフギミックのクラス
/// </summary>
public class KnifeGimmick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            SoundManager.Instance.PlaySE(SESource.CUT);

            //お餅を切って、サイズを縮小
            _ricecake.CutRicecake();

            //ナイフ自体は消去
            gameObject.SetActive(false);
        }
    }
}
