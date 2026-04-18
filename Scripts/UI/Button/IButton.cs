using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボタンを定義するインタフェース
/// </summary>
public interface IButton
{
    void ButtonEnter();
    void ButtonDown();
    void ButtonClick();
    void ButtonUp();
    void ButtonExit();
}
