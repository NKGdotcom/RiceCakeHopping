using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextColorChange:MonoBehaviour
{
    private string textColor = "FaceColor";

    protected float colorChangeDuration = 0.1f;
    private Color32 defaultBlackTextColor = new Color32(0, 0, 0, 255);
    private Color32 changeBlackColorText = new Color32(255, 130, 130, 255); // 黒いテキストをホバーしたときに変わる色
    private Color32 defaultWhiteTextColor = new Color32(255, 255, 255, 255);
    private Color32 changeWhiteColorText = new Color32(255, 130, 130, 255); // 白いテキストをホバーしたときに変わる色

    private Coroutine currentColorChangeCoroutine; //現在実行中の色が変わるアニメーションコルーチン

    // Start is called before the first frame update
    protected virtual void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// イベントトリガーの処理を追加します。
    /// </summary>
    /// <param name="_target">UI GameObject</param>
    /// <param name="_eventType">使用するイベントトリガーのタイプ</param>
    /// <param name="_action">実行したい関数</param>
    protected void AddEventTrigger(GameObject _target, EventTriggerType _eventType, UnityAction<BaseEventData> _action)
    {
        EventTrigger _trigger = _target.GetComponent<EventTrigger>() ?? _target.AddComponent<EventTrigger>();
        EventTrigger.Entry _entry = new EventTrigger.Entry { eventID = _eventType };
        _entry.callback.AddListener(_action);
        _trigger.triggers.Add(_entry);
    }
    /// <summary>
    /// テキストのイベント処理を設定します。
    /// </summary>
    /// <param name="_text">対象のテキストGameObject</param>
    /// <param name="_enterAction">テキストの上にマウスが置かれたときに実行されるアクション</param>
    /// <param name="_exitAction">テキストからマウスが離れたときに実行されるアクション</param>
    /// <param name="_clickAction">テキストがクリックされたときに実行されるアクション</param>
    protected void SetUpTextEvent(GameObject _text, UnityAction<BaseEventData> _enterAction, UnityAction<BaseEventData> _exitAction, UnityAction<BaseEventData> _clickAction)
    {
        AddEventTrigger(_text, EventTriggerType.PointerEnter, _enterAction);
        AddEventTrigger(_text, EventTriggerType.PointerExit, _exitAction);
        AddEventTrigger(_text, EventTriggerType.PointerClick, _clickAction);
    }

    /// <summary>
    /// テキストの色を徐々に変更するコルーチンです。
    /// </summary>
    /// <param name="_text">対象のTextMeshProUGUIコンポーネント</param>
    /// <param name="_targetColor">変更後の目標の色</param>
    /// <param name="_duration">色が完全に変わるまでの時間</param>
    /// <returns></returns>
    private IEnumerator ChangeTextColorOverTime(TextMeshProUGUI _text, Color32 _targetColor, float _duration)
    {
        Color32 startColor = _text.color;
        float _elapsed = 0f;

        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            float _t = _elapsed / _duration;
            _text.color = Color.Lerp(startColor, _targetColor, _t);
            // マテリアルの色も補間することで、テキスト全体の色が滑らかに変わるようにします
            _text.fontMaterial.SetColor(textColor, _text.color);
            yield return null;
        }

        // 最終的な色に設定して、補間の誤差をなくします
        _text.color = _targetColor;
        _text.fontMaterial.SetColor(textColor, _targetColor);
    }

    /// <summary>
    /// テキストの色を変更するコルーチンを開始します。
    /// 既存の色変更コルーチンがある場合は、それを停止してから新しいコルーチンを開始します。
    /// これにより、複数の色変更が同時に走るのを防ぎ、スムーズな遷移を保証します。
    /// </summary>
    /// <param name="_text">対象のTextMeshProUGUIコンポーネント</param>
    /// <param name="_targetColor">目標の色</param>
    protected void StartColorChange(TextMeshProUGUI _text, Color32 _targetColor)
    {
        currentColorChangeCoroutine = StartCoroutine(ChangeTextColorOverTime(_text, _targetColor, colorChangeDuration));
    }

    /// <summary>
    /// 黒いテキストの色を変化させる
    /// </summary>
    /// <param name="_text">対象のTextMeshProUGUIコンポーネント</param>
    public void ChangeBlackText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, changeBlackColorText);
    }

    /// <summary>
    /// 黒色に戻します。
    /// </summary>
    /// <param name="_text">対象のTextMeshProUGUIコンポーネント</param>
    public void ResetBlackText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, defaultBlackTextColor);
    }

    /// <summary>
    /// 白いテキストの色を変化させる。
    /// </summary>
    /// <param name="_text">対象のTextMeshProUGUIコンポーネント</param>
    public void ChangeWhiteText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, changeWhiteColorText);
    }

    /// <summary>
    /// 白色に戻す。
    /// </summary>
    /// <param name="_text">対象のTextMeshProUGUIコンポーネント</param>
    public void ResetWhiteText(TextMeshProUGUI _text)
    {
        StartColorChange(_text, defaultWhiteTextColor);
    }
}