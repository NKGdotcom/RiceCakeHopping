using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<RicecakeObject>(out RicecakeObject ricecake))
        {
            ricecake.CutRiceCake();
            gameObject.SetActive(false);
        }
    }
}
