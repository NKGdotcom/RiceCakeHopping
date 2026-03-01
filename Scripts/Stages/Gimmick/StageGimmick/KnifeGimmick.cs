using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ナイフギミッククラス
/// </summary>
public class KnifeGimmick : MonoBehaviour, IGimmick
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            _ricecake.CutRicecake();
            gameObject.SetActive(false);
        }
    }
}
