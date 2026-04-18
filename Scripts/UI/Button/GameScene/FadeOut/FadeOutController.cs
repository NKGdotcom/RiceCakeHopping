using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// フェードアウトの管理
/// </summary>
public class FadeOutController : MonoBehaviour
{
    [Header("アニメーション")]
    [Tooltip("フェードアウトアニメーション")]
    [SerializeField] private Animator fadeAnimator;

    //フェードアウトをした後一瞬待機
    private float fadeoutDelay = 0.5f;
    private const string STR_FADE_OUT = "FadeOut";

    /// <summary>
    /// フェードアウトが完了するまで待機する非同期処理
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask WaitFadeOutAsync( CancellationToken _token)
    {
        fadeAnimator.SetTrigger(STR_FADE_OUT);
        await UniTask.WaitForSeconds(fadeoutDelay, cancellationToken: _token);
    }
}
