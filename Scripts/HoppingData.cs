using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoppingData : ScriptableObject
{
    public float RotationSpeed { get => rotationSpeed; private set => rotationSpeed = value; }
    public float BouncePower { get => bouncePower; private set => bouncePower = value; }
    public float RicecakeKnockbackPower { get => ricecakeKnockbackPower; private set => ricecakeKnockbackPower = value; }
    public float SmoothRotation { get => smoothRotation; private set => smoothRotation = value; }

    [SerializeField] private float rotationSpeed; //傾けるスピード
    [SerializeField] private float bouncePower; //バウンドする力
    [SerializeField] private float ricecakeKnockbackPower; //餅を飛ばす力
    [SerializeField] private float smoothRotation; //なめらかな回転
}
