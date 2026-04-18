using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ボタンの基本実装。UIイベントを受け取り、共通処理を行いつつ派生クラスへ処理を渡す
/// </summary>
public class BaseButton : MonoBehaviour, IButton, IPointerEnterHandler,IPointerDownHandler,IPointerClickHandler,IPointerUpHandler,IPointerExitHandler
{
    //インタフェースの実装
    public void OnPointerEnter(PointerEventData eventData) => ButtonEnter();
    public void OnPointerDown(PointerEventData eventData) => ButtonDown();
    public void OnPointerClick(PointerEventData eventData) => ButtonClick();
    public void OnPointerUp(PointerEventData eventData) => ButtonUp();
    public void OnPointerExit(PointerEventData eventData) => ButtonExit();

    //子クラスで上書き
    public virtual void ButtonEnter() { }
    public virtual void ButtonDown() { }
    public virtual void ButtonClick() { }
    public virtual void ButtonUp() { }
    public virtual void ButtonExit() { }
}
