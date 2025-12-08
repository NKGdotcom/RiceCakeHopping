using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinakoPaint : TastePaint
{
    
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<RicecakeObject>(out  RicecakeObject ricecake))
        {
            base.OnTriggerEnter(other);
            ricecake.ChangeTaste(StageData.RicecakeType.Kinako); //–¡•Ï
            ricecake.StopRiceCake();
            gameObject.SetActive(false);
        }
    }
}
