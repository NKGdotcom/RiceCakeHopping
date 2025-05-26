using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeDropDown : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    private Vector3 initialPos;
    private Rigidbody rigidbody;
    private void Start()
    {
        initialPos = this.transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!stageManager.IsPause)
        {
            rigidbody.AddForce(new Vector3(0f, -9.8f, 0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            this.transform.position = initialPos;
            rigidbody.velocity = Vector3.zero;
        }
    }
}
