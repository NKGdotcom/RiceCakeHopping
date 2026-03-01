using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// フェードアウト用のスクリプト
/// </summary>
public class FadeOutController : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    private float fadeoutSpeed = 0.5f;
    private const string STR_FADE_OUT = "FadeOut";

    public async UniTask WaitFadeOutAsync( CancellationToken _token)
    {
        fadeAnimator.SetTrigger(STR_FADE_OUT);
        await UniTask.WaitForSeconds(fadeoutSpeed, cancellationToken: _token);
    }
}
