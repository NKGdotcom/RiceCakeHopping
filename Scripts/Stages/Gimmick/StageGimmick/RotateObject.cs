using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトの回転ギミック
/// </summary>
public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}