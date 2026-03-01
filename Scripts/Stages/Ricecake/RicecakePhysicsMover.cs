using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ñ›ÇÃï®óùãììÆ
/// </summary>
public class RicecakePhysicsMover : MonoBehaviour
{
    private Rigidbody ricecakeRb;
    private float ricecakeForcePower = 20f;
    private float reflectPower = 15f;
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Rigidbody>(out ricecakeRb);
    }

    public void AddForceRicecakeCol(Collision _collision)
    {
        if (ricecakeRb == null) return;

        Vector3 _contactPoint = _collision.contacts[0].point;
        Vector3 _forceDir = (_collision.transform.position - _contactPoint).normalized;

        _forceDir = new Vector3(_forceDir.x, 0, _forceDir.z).normalized;
        ricecakeRb.AddForce(_forceDir * ricecakeForcePower, ForceMode.Impulse);
    }
    public void AddForceReflect(Vector3 _reflectPos)
    {
        Vector3 _forceDir = (transform.position - _reflectPos).normalized;
        ricecakeRb.AddForce(_forceDir * reflectPower, ForceMode.Impulse);
    }

    public void StopForce()
    {
        if(ricecakeRb == null) return;

        ricecakeRb.velocity = Vector3.zero;
        ricecakeRb.angularVelocity = Vector3.zero;
    }
}
