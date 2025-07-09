using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnifeDropDown : MonoBehaviour
{
    private Vector3 initialPos;
    private Rigidbody rigidbody;
    private float gravity = 9.8f;
    private void Start()
    {
        initialPos = this.transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameStateMachine.Instance.IsPlaying()||GameStateMachine.Instance.IsIntroduceClear())
        {
            rigidbody.AddForce(new Vector3(0f, -gravity, 0f));
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
