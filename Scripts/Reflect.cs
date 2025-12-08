using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<RicecakeObject>(out  RicecakeObject ricecake))
        {
            ricecake.OnHitByReflect(transform.position);
        }
    }
}
