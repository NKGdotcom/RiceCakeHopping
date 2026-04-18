using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズボタン自体のアニメーション
/// </summary>
public class PauseButtonAnimation : MonoBehaviour
{
    [Header("アニメーション")]
    [Tooltip("ポーズボタンの色を変えるアニメーション")]
    [SerializeField] private Animator pauseButtonAnimator;
    private const string STR_BUTTON_ENTER = "ButtonEnter";

    // Start is called before the first frame update
    void Awake()
    {
        if(pauseButtonAnimator == null) { TryGetComponent<Animator>(out pauseButtonAnimator); }
    }

    /// <summary>
    /// ポーズボタンに入ったら色を変える
    /// </summary>
    public void PauseButtonEnter()
    {
        pauseButtonAnimator.SetBool(STR_BUTTON_ENTER, true);
    }

    /// <summary>
    /// ポーズボタンから抜けたら色を元に戻す
    /// </summary>
    public void PauseButtonExit()
    {
        pauseButtonAnimator.SetBool(STR_BUTTON_ENTER, false);
    }
}
