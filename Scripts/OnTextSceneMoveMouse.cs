using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//テキストを押したらシーンが変わる時のベーススクリプト
public class OnTextSceneMoveMouse : OnTextMouse
{
    [SerializeField] private Animator fadeAnimator;
    private string fadeOutStr = "FadeOut";
    
    // Start is called before the first frame update

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
    }
    /// <summary>
    /// 名前を付けないもの
    /// </summary>
    public void SetFadeAnimation()
    {
        fadeAnimator.SetTrigger(fadeOutStr);
    }
    /// <summary>
    /// 名前を付けるもの
    /// </summary>
    /// <param name="_fadeOutStr"></param>
    public void SetFadeAnimation(string _fadeOutStr)
    {
        fadeAnimator.SetTrigger(_fadeOutStr);
    }
    public virtual IEnumerator WaitStart(string _sceneName)
    {
        yield return null;
        AnimatorStateInfo animInfo = fadeAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(animInfo.length);
        SceneManager.LoadScene(_sceneName);
        yield break;
    }
}
