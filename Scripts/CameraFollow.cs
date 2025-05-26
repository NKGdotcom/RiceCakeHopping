using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player; 
    [SerializeField] Vector3 offset;  

    void LateUpdate()
    {
        if (player != null)
        {
            // プレイヤーの位置に基づいてカメラを移動
            transform.position = player.position + offset;
        }
    }
}
