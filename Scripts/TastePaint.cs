using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//味付けのベーススクリプト
public class TastePaint : MonoBehaviour
{
    [SerializeField] private Material tasteMaterial;
    [SerializeField] private GameObject hitParticle;
    public virtual void OnTriggerEnter(Collider other)
    {
        MeshRenderer _riceCakeTasteMaterial = other.gameObject.GetComponent<MeshRenderer>();
        if(tasteMaterial != null) _riceCakeTasteMaterial.material = tasteMaterial;
        if(hitParticle != null) Instantiate(hitParticle, other.transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
}
