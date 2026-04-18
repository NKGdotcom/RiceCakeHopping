using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズ画面を開くアニメーション
/// </summary>
public class PauseAnimation : MonoBehaviour
{
    [Header("アニメーション")]
    [Tooltip("ポーズ画面を開くアニメーション")]
    [SerializeField] private Animator pauseAnimator;
    private const string STR_DISPLAY_UI = "DisplayUI";

    private void Awake()
    {
        if(pauseAnimator == null) { TryGetComponent<Animator>(out pauseAnimator); }
    }

    /// <summary>
    /// ポーズ画面を開くアニメーション
    /// </summary>
    public void PauseOpen()
    {
        pauseAnimator.SetBool(STR_DISPLAY_UI, true);
    }

    /// <summary>
    /// ポーズ画面を閉じるアニメーション
    /// </summary>
    public void PauseClose()
    {
        pauseAnimator.SetBool(STR_DISPLAY_UI, false);
    }
}
