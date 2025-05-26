using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCakeUnderDesk : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    private void OnTriggerEnter(Collider other)
    {
        if (!stageManager.IsResult)
        {
            stageManager.IsResult = true;
            StartCoroutine(stageManager.NotEatRiceCake());
        }
    }
}
