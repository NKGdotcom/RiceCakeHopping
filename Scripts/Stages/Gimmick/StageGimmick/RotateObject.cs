using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトを回転させるギミックのクラス
/// </summary>
public class RotateObject : MonoBehaviour
{
    [Header("回転設定")]
    [Tooltip("回転する速さ")]
    [SerializeField] private float rotateSpeed = 50;

    void Update()
    {
        Rotate();
    }

    /// <summary>
    /// その場で回転
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}