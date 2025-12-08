using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDropDown : MonoBehaviour
{
    private Vector3 initialPos;
    private Rigidbody knifeRigidbody;
    private float gravity = 9.8f;

    private float fallInterval = 3;
    private float timer;
    private void Start()
    {
        timer = fallInterval;
        initialPos = this.transform.position;
        knifeRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            InitialSetPos();
            timer = fallInterval;
        }
            knifeRigidbody.AddForce(new Vector3(0f, -gravity, 0f));
    }

    private void InitialSetPos()
    {
        this.transform.position = initialPos;
        knifeRigidbody.velocity = Vector3.zero;
    }
}
