using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 机の↓への落下判定を行うギミック
/// </summary>
public class ResultUnderdesk : MonoBehaviour
{
    /// <summary>
    /// アイテムが机の下に落ちたときに発行されるイベント
    /// </summary>
    public event Action OnItemFall;

    private void OnTriggerEnter(Collider other)
    {
        //餅が落ちても、条件を達成できない
        if(other.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            OnItemFall?.Invoke();
        }

        //ホッピングが落ちても条件を達成できない
        else if(other.gameObject.TryGetComponent<HoppingController>(out var _hopping))
        {
            OnItemFall?.Invoke();
        }
    }
}
