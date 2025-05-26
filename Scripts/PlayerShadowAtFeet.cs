using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowAtFeet : MonoBehaviour //�z�b�s���O�̑����ɉe��ݒu
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private float shadowHeight = 0.5f; //�e�̍���

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(playerTransform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point + Vector3.up * shadowHeight;
        }
    }
}
