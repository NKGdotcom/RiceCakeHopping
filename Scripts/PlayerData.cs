using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData",menuName ="ScriptableObjects/Player/PlayerData")]
public class PlayerData : ScriptableObject 
{
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float BouncePower { get => bouncePower; set => bouncePower = value; }
    public float RicaCakeKnockbackPower { get => ricaCakeKnockbackPower; set => ricaCakeKnockbackPower = value; }

    [Header("傾けるスピード")]
    [SerializeField] private float rotationSpeed;
    [Header("バウンドする力")]
    [SerializeField] private float bouncePower;
    [Header("餅を吹っ飛ばす時の力")]
    [SerializeField] private float ricaCakeKnockbackPower;
}
