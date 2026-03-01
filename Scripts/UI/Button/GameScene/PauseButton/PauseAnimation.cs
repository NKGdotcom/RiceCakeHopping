using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OpenPauseAnimation ポーズを開くアニメーション
/// </summary>
public class PauseAnimation : MonoBehaviour
{
    [SerializeField] private Animator pauseAnimator;
    private const string STR_DISPLAY_UI = "DisplayUI";

    private void Awake()
    {
        if(pauseAnimator == null) { TryGetComponent<Animator>(out pauseAnimator); }
    }

    //ポーズ画面を開く
    public void PauseOpen()
    {
        pauseAnimator.SetBool(STR_DISPLAY_UI, true);
    }

    public void PauseClose()
    {
        pauseAnimator.SetBool(STR_DISPLAY_UI, false);
    }
}
