using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊び方のページで押すと1ページ分進む
/// </summary>
public class RightButtonArrowContoroller : BaseButton
{
    /// <summary>
    /// ボタンを押したことを知らせる
    /// </summary>
    public event Action OnClicked;

    /// <summary>
    /// ボタンを押すと次のページに進む
    /// </summary>
    public override void ButtonClick()
    {
        SoundManager.Instance.PlaySE(SESource.BUTTON);
        OnClicked?.Invoke();
    }
}
