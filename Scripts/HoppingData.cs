using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �z�b�s���O�̃f�[�^
/// </summary>
[CreateAssetMenu(fileName = "HoppingData",menuName ="ScriptableObjects/Hopping/HoppingData")]
public class HoppingData : ScriptableObject 
{
    public float RotationSpeed { get => rotationSpeed; private set => rotationSpeed = value; }
    public float BouncePower { get => bouncePower; private set => bouncePower = value; }
    public float RiceCakeKnockbackPower { get => riceCakeKnockbackPower; private set => riceCakeKnockbackPower = value; }
    public float SmoothRotation { get => smoothRotation; private set => smoothRotation = value; }

    [Header("�X����X�s�[�h")]
    [SerializeField] private float rotationSpeed;
    [Header("�o�E���h�����")]
    [SerializeField] private float bouncePower;
    [Header("�݂𐁂���΂����̗�")]
    [SerializeField] private float riceCakeKnockbackPower;
    [Header("�ǂ̂��炢���炩�ɉ�]�����邩")]
    [SerializeField] private float smoothRotation;
}
