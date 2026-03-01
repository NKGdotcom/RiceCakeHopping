using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングのカメラの挙動
/// </summary>
public class HoppingCameraFollow : MonoBehaviour
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
    /// 遅れて追いかける
    /// </summary>
    private void LateChaseCamera()
    {
        transform.position = hopping.transform.position + cameraOffset;
    }
}
