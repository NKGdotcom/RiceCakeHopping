using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowAtFeet : MonoBehaviour //ホッピングの足元に影を設置
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private float shadowHeight = 0.5f; //影の高さ

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
