using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅の色を変更する
/// </summary>
public class RicecakeChangeColor : MonoBehaviour
{
    [SerializeField] private RicecakeDataList ricecakeDataList;
    [SerializeField] private RicecakeHitEffect ricecakeHitEffect;

    private void Awake()
    {
        if (ricecakeDataList == null) { Debug.LogError("ricecakeDataListが参照されていません。"); return; }
        if (ricecakeHitEffect == null) { TryGetComponent<RicecakeHitEffect>(out ricecakeHitEffect); }
    }

    public void ChangeRicecakeColor(RicecakeType _ricecakeType)
    {
        foreach(var _data in ricecakeDataList.DataList)
        {
            if(_data.RicecakeType == _ricecakeType)
            {
                GetComponent<Renderer>().material = _data.RicecakeMaterial;
                ricecakeHitEffect.SetEffectPrefab(_data.HitEffectPrefab);
                break;
            }
        }
    }
}
