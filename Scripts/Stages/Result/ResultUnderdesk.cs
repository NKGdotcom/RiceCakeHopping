using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 机より下に触れた場合、食えないじゃないかとリザルト
/// </summary>
public class ResultUnderdesk : MonoBehaviour
{
    public event Action IsItemFall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            IsItemFall?.Invoke();
        }
        else if(other.gameObject.TryGetComponent<HoppingController>(out var _hopping))
        {
            IsItemFall?.Invoke();
        }
    }
}
