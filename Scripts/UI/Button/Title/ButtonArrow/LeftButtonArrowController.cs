using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ç∂ñÓàÛ
/// </summary>
public class LeftButtonArrowController : BaseButton
{
    public event Action BackPage;

    public override void ButtonClick()
    {
        BackPage?.Invoke();
    }
}
