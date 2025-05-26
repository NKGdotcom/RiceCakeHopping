using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData",menuName ="ScriptableObjects/Player/PlayerData")]
public class PlayerData : ScriptableObject 
{
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float BouncePower { get => bouncePower; set => bouncePower = value; }
    public float RicaCakeKnockbackPower { get => ricaCakeKnockbackPower; set => ricaCakeKnockbackPower = value; }

    [Header("�X����X�s�[�h")]
    [SerializeField] private float rotationSpeed;
    [Header("�o�E���h�����")]
    [SerializeField] private float bouncePower;
    [Header("�݂𐁂���΂����̗�")]
    [SerializeField] private float ricaCakeKnockbackPower;
}
