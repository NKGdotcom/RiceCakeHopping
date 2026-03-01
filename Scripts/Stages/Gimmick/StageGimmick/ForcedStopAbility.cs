using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// •Ç‚È‚Ç‚Å‚Æ‚Ç‚ß‚½‚¢‚Æ‚±‚ë‚Å‚Â‚¯‚é
/// </summary>
public class ForcedStopAbility : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(TryGetComponent<IRiceCake>(out var _ricacake))
        {
            _ricacake.StopRicecake();
        }
    }
}
