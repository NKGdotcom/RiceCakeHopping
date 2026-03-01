using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ïÔíöÇ™ÅAé©óRóéâ∫Ç≈óéÇøÇÈ
/// </summary>
public class KnifeDropDown : MonoBehaviour
{
    private Vector3 initialPos;
    private Rigidbody knifeRigidbody;

    private float gravity = 9.8f;
    private float fallInterval = 3;
    private float timer;
    private void Awake()
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
    }

    private void InitialSetPos()
    {
        this.transform.position = initialPos;
        knifeRigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        FreeFall();
    }

    //é©óRóéâ∫
    private void FreeFall()
    {
        knifeRigidbody.AddForce(new Vector3(0f, -gravity, 0f));
    }
}
