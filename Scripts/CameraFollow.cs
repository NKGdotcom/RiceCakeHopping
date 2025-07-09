using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform hopping; 
    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - hopping.transform.position; //カメラのオフセット取得
    }
    void LateUpdate()
    {
        transform.position = hopping.transform.position + cameraOffset;
    }
}
