using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 反射させたいオブジェクトに対して
/// </summary>
public class ReflectGimmick : MonoBehaviour, IGimmick
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            _ricecake.OnHitByReflect(transform.position);
        }
    }
}
