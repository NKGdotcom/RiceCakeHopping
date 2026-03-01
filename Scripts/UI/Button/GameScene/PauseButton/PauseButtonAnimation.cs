using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズボタンのアニメーション
/// 色が変わるなど
/// </summary>
public class PauseButtonAnimation : MonoBehaviour
{
    [SerializeField] private Animator pauseButtonAnimator;
    private const string STR_BUTTON_ENTER = "ButtonEnter";

    // Start is called before the first frame update
    void Awake()
    {
        if(pauseButtonAnimator == null) { TryGetComponent<Animator>(out pauseButtonAnimator); }
    }

    public void PauseButtonEnter()
    {
        Debug.Log(pauseButtonAnimator);
        pauseButtonAnimator.SetBool(STR_BUTTON_ENTER, true);
    }
    public void PauseButtonExit()
    {
        pauseButtonAnimator.SetBool(STR_BUTTON_ENTER, false);
    }
}
