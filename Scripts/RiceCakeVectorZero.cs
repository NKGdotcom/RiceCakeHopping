using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ǂȂǂɐG�ꂽ��݂̑��x���~�߂�
/// </summary>
public class RiceCakeVectorZero : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Normal")|| collision.gameObject.CompareTag("KinakoRiceCake")|| collision.gameObject.CompareTag("SoySourceRiceCake"))
        {
            Rigidbody _rb = collision.gameObject.GetComponent<Rigidbody>();
            _rb.velocity = Vector3.zero;
        }
    }
}
