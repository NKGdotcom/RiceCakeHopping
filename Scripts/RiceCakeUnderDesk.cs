using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCakeUnderDesk : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GameResult.Instance.NotEatRiceCake()); //�݂�z�b�s���O����������
    }
}
