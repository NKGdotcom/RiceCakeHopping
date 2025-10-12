using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの下に置く影でホッピングの位置関係をわかりやすく
/// </summary>
public class HoppingShadowAtFeet : MonoBehaviour //位置が分かりやすいように
{
    [Header("影")]
    [SerializeField] private Transform feetShadow;
    [Header("ホッピングの場所")]
    [SerializeField] private Transform hoppingTransform;
    [Header("地面から影までの高さ")]
    [SerializeField] private float shadowHeight = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit _hit;
        if (Physics.Raycast(hoppingTransform.transform.position, Vector3.down, out _hit, Mathf.Infinity))
        {
            feetShadow.transform.position = _hit.point + Vector3.up * shadowHeight;
        } 
    }
}
