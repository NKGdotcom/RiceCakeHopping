using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ‰E–îˆóƒ{ƒ^ƒ“
/// </summary>
public class RightButtonArrowContoroller : BaseButton
{
    public event Action ProceedPage;

    public override void ButtonClick()
    {
        ProceedPage?.Invoke();
    }
}
