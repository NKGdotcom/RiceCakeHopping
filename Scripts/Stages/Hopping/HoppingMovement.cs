using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングを傾けたら移動する挙動
/// </summary>
public class HoppingMovement : MonoBehaviour
{
    [Header("パラメータ設定(デバッグ用)")]
    [Tooltip("傾きのスピード")]
    [SerializeField] private float rotationSpeed;
    [Tooltip("傾ける滑らかさ")]
    [SerializeField] private float smoothRotation;

    //ホッピングの傾き情報
    private float currentXRot;
    private float currentZRot;

    /// <summary>
    /// 初期データのパラメータ設定
    /// </summary>
    /// <param name="_data"></param>
    public void SetUp(HoppingData _data)
    {
        rotationSpeed = _data.RotationSpeed;
        smoothRotation = _data.SmoothRotation;
    }

    /// <summary>
    /// ホッピングを傾けながらの移動挙動
    /// </summary>
    public void HoppingMoveTilt()
    {
        //GetAxisを通じて傾きを滑らかに回転できるように
        float _targetXRot = Input.GetAxis("Vertical") * rotationSpeed;
        float _targetZRot = -Input.GetAxis("Horizontal") * rotationSpeed;

        //線形を用いた滑らかな傾きの移動
        currentXRot = Mathf.Lerp(currentXRot, _targetXRot, Time.deltaTime * smoothRotation);
        currentZRot = Mathf.Lerp(currentZRot, _targetZRot, Time.deltaTime * smoothRotation);

        transform.Rotate(new Vector3(currentXRot, 0, currentZRot) * Time.deltaTime);
    }
}
