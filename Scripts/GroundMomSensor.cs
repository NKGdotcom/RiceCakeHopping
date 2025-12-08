using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMomSensor : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent<RicecakeObject>(out RicecakeObject ricecake))
        {
            stageManager.CheckClearCondition(ricecake);
        }
        else
        {
            stageManager.SomethingEat();
        }
        collision.gameObject.SetActive(false);
    }
}
