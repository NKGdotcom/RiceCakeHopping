using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// テキストやボタンなどなんでもトリガーを追加したい場合に入れる
/// </summary>
public class OnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// マウスが上に置かれたら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }

    /// <summary>
    /// マウスが上から離れたら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }

    /// <summary>
    /// マウスがクリックされたら
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
    }
}
