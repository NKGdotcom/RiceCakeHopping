using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderDesk : MonoBehaviour
{
    [SerializeField] private ResultManager resultManager;

    private void OnTriggerEnter(Collider other)
    {
        if (resultManager != null)
        {
            gameObject.SetActive(false);
            resultManager.ShowResult(ResultType.NotEat);
        }
    }
}
