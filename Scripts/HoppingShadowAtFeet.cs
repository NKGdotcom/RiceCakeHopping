using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �z�b�s���O�̉��ɒu���e�Ńz�b�s���O�̈ʒu�֌W���킩��₷��
/// </summary>
public class HoppingShadowAtFeet : MonoBehaviour //�ʒu��������₷���悤��
{
    [Header("�e")]
    [SerializeField] private Transform feetShadow;
    [Header("�z�b�s���O�̏ꏊ")]
    [SerializeField] private Transform hoppingTransform;
    [Header("�n�ʂ���e�܂ł̍���")]
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
