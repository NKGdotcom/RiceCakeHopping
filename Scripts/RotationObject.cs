using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���̏��y���ɉ�]
/// </summary>
public class RotationObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
