using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの物理を必要とする動作をここに追加
/// </summary>
public class HoppingPhysicsMover : MonoBehaviour
{
    private Rigidbody hoppingRb;
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Rigidbody>(out hoppingRb);
    }

    public void Jump(Vector3 _jumpDirection)
    {
        hoppingRb.velocity = _jumpDirection;
    }
}
