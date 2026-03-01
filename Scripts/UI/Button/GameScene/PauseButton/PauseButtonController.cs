using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズボタンの処理をここにまとめる
/// </summary>
public class PauseButtonController : BaseButton
{
    [SerializeField] private PauseButtonAnimation pauseButtonAnimation;
    [SerializeField] private PauseAnimation pauseAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        if(pauseButtonAnimation == null) { TryGetComponent<PauseButtonAnimation>(out pauseButtonAnimation); }
        if (pauseAnimation == null) { TryGetComponent<PauseAnimation>(out pauseAnimation); }
    }

    public override void ButtonEnter()
    {
        pauseButtonAnimation.PauseButtonEnter();
    }

    public override void ButtonExit()
    {
        pauseButtonAnimation.PauseButtonExit();
    }

    public override void ButtonClick()
    {
        StageStateController.Instance.ChangeState(StageState.GAME_PAUSE);
        Time.timeScale = 0;
        pauseAnimation.PauseOpen();
    }
}
