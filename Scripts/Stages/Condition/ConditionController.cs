using TMPro;
using UnityEngine;

/// <summary>
/// クリア条件をテキストに反映
/// </summary>
public class ConditionController : MonoBehaviour
{
    [Header("UI参照")]
    [Tooltip("クリア条件を表示するコンポーネント")]
    [SerializeField] private TextMeshProUGUI conditionTMP;

    private void Awake()
    {
        if(conditionTMP == null) { Debug.LogError("conditionTMPが参照されていません"); return; }
    }

    /// <summary>
    /// 渡された文字列をUIのテキストに反映する
    /// </summary>
    /// <param name="_conditionStr"></param>
    public void SetCondition(string _conditionStr)
    {
        conditionTMP.text = _conditionStr;
    }
}
