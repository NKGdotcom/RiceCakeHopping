using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅に触れた際のエフェクトを発火
/// </summary>
public class RicecakeHitEffect : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if(hitEffectPrefab == null) { Debug.LogError("hitEffectPrefabが参照されていません"); return; }
    }
    public void SetEffectPrefab(GameObject _hitEffectPrefab)
    {
        hitEffectPrefab = _hitEffectPrefab;
    }

    public void ShowHitParticle(RicecakeType ricecakeType)
    {
        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
    }
}
