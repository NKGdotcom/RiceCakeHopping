using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform hopping;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - hopping.transform.position;
    }

    private void LateUpdate()
    {
        LateChaseCamera();
    }
    /// <summary>
    /// ’x‚ê‚Ä’Ç‚¢‚©‚¯‚é
    /// </summary>
    private void LateChaseCamera()
    {
        transform.position = hopping.transform.position + cameraOffset;
    }
}
