using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お餅を反射させるギミックのクラス　
/// </summary>
public class ReflectGimmick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            //お餅を持っていたら反射させる
            _ricecake.OnHitByReflect(transform.position);
        }
    }
}
