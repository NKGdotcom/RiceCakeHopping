using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// クリア条件をテキストに反映
/// </summary>
public class ConditionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI conditionTMP;

    public void SetCondition(string _conditionStr)
    {
        conditionTMP.text = _conditionStr;
    }
}
