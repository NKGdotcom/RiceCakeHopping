using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの移動について
/// ホッピングは傾いたら移動
/// </summary>
public class HoppingMovement : MonoBehaviour
{
    private float rotationSpeed;
    private float smoothRotation;
    private float currentXRot;
    private float currentZRot;

    public void SetUp(HoppingData _data)
    {
        rotationSpeed = _data.RotationSpeed;
        smoothRotation = _data.SmoothRotation;
    }

    //傾きを動かす
    //_targetRotはinput×rotationSpeedの値を少しずつ加算や減算し、
    //回転を現在回転位置から近づける
    //(完全に近づくことはないが、近づくほど回転が遅くなる)
    public void HoppingMoveTilt()
    {
        float _targetXRot = Input.GetAxis("Vertical") * rotationSpeed;
        float _targetZRot = -Input.GetAxis("Horizontal") * rotationSpeed;

        currentXRot = Mathf.Lerp(currentXRot, _targetXRot, Time.deltaTime * smoothRotation);
        currentZRot = Mathf.Lerp(currentZRot, _targetZRot, Time.deltaTime * smoothRotation);

        transform.Rotate(new Vector3(currentXRot, 0, currentZRot) * Time.deltaTime);
    }
}
