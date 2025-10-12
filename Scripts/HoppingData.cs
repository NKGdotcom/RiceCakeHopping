using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングのデータ
/// </summary>
[CreateAssetMenu(fileName = "HoppingData",menuName ="ScriptableObjects/Hopping/HoppingData")]
public class HoppingData : ScriptableObject 
{
    public float RotationSpeed { get => rotationSpeed; private set => rotationSpeed = value; }
    public float BouncePower { get => bouncePower; private set => bouncePower = value; }
    public float RiceCakeKnockbackPower { get => riceCakeKnockbackPower; private set => riceCakeKnockbackPower = value; }
    public float SmoothRotation { get => smoothRotation; private set => smoothRotation = value; }

    [Header("傾けるスピード")]
    [SerializeField] private float rotationSpeed;
    [Header("バウンドする力")]
    [SerializeField] private float bouncePower;
    [Header("餅を吹っ飛ばす時の力")]
    [SerializeField] private float riceCakeKnockbackPower;
    [Header("どのくらい滑らかに回転させるか")]
    [SerializeField] private float smoothRotation;
}
