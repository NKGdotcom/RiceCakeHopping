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
            // �v���C���[�̈ʒu�Ɋ�Â��ăJ�������ړ�
            transform.position = player.position + offset;
        }
    }
}
