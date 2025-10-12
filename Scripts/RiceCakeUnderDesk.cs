using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ちゃぶ台の下に置いたとき
/// </summary>
public class RiceCakeUnderDesk : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GameResult.Instance.NotEatRiceCake()); //餅やホッピングが落ちたら
    }
}
