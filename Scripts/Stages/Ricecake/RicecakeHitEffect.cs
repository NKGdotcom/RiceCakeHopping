using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅がホッピングや餅同士が触れた際にエフェクトを発火
/// </summary>
public class RicecakeHitEffect : MonoBehaviour
{
    [Header("エフェクト参照")]
    [Tooltip("ホッピングや餅に触れた際に発火するパーティクルのPrefab")]
    [SerializeField] private GameObject hitEffectPrefab;

    private float hitDelayDestroyTime = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        if(hitEffectPrefab == null) { Debug.LogError("hitEffectPrefabが参照されていません"); return; }
    }

    /// <summary>
    /// 基本は白色、もし味付けが変わった場合は色を変える
    /// </summary>
    /// <param name="_hitEffectPrefab"></param>
    public void SetEffectPrefab(GameObject _hitEffectPrefab)
    {
        hitEffectPrefab = _hitEffectPrefab;
    }

    /// <summary>
    /// パーティクルを発火させる
    /// </summary>
    /// <param name="ricecakeType"></param>
    public void ShowHitParticle(RicecakeType ricecakeType)
    {
        GameObject _particlePrefab = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        
        //少ししたらパーティクルを破壊する
        Destroy(_particlePrefab, hitDelayDestroyTime);
    }
}
