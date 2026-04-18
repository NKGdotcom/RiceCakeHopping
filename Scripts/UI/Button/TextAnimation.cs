using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テキストの色が変わるアニメーションの管理
/// </summary>
public class TextAnimation : MonoBehaviour
{
    [Header("アニメーション")]
    [Tooltip("テキストのアニメーション")]
    [SerializeField] private Animator textAnimator;
    
    //テキストを赤色に変更
    private const string STR_CHANGE_RED = "ChangeRed";

    void Awake()
    {
        if (textAnimator == null) { Debug.LogError("textAnimatorが参照されていません。"); return; }
    }

    /// <summary>
    /// テキストを赤色に変更
    /// </summary>
    public void RedChangeColor()
    {
        textAnimator.SetBool(STR_CHANGE_RED, true);
    }

    /// <summary>
    /// テキストを元の色に変更
    /// </summary>
    public void RedChangeDefaultColor()
    {
        textAnimator.SetBool (STR_CHANGE_RED, false);
    }
}
