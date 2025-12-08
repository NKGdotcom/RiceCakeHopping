using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeStop : MonoBehaviour
{
    private bool canStop = true;

    //ˆê‰ñ‚¾‚¯Ž~‚ß‚é
    private void OnCollisionEnter(Collision collision)
    {
        if (!canStop) return;
        if(collision.gameObject.TryGetComponent<RicecakeObject>(out RicecakeObject ricecake))
        {
            canStop = false;
            ricecake.StopRiceCake();
        }
    }
}
