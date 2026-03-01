using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホッピングの動きを管理するクラス
/// 基本はここでのみStartやUpdateを使う
/// </summary>
public class HoppingController : MonoBehaviour
{
    [SerializeField] private HoppingData hoppingData;

    [SerializeField] private HoppingMovement hoppingMovement;
    [SerializeField] private HoppingJump hoppingJump;
    [SerializeField] private HoppingShadowFeet hoppingShadowFeet;

    // Start is called before the first frame update
    void Awake()
    {
        if (hoppingData == null) { Debug.LogError("hoppingDataが参照されていません"); return; }
        if (hoppingMovement == null) { TryGetComponent<HoppingMovement>(out hoppingMovement); }
        if (hoppingJump == null) { TryGetComponent<HoppingJump>(out hoppingJump); }
        if(hoppingShadowFeet == null) { Debug.LogError("hoppingShadowFeetが参照されていません"); return; }
        
        hoppingMovement.SetUp(hoppingData);
        hoppingJump.SetUp(hoppingData);
    }

    // Update is called once per frame
    void Update()
    {
        hoppingMovement.HoppingMoveTilt();

        UpdateShadow();
    }

    //下から伸ばされたRayがホッピングにあたっているか
    //ホッピングの足元に影を置くため
    private void UpdateShadow()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            hoppingShadowFeet.ShowShadow();
            hoppingShadowFeet.FeetShadow(hit);
        }
        else
        {
            hoppingShadowFeet.HideShadow();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hoppingJump.HoppingJumpMovement();

        if(collision.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            _ricecake.OnHitByHopping(collision);
            SoundManager.Instance.PlaySE(SESource.riceCakeCollision);
        }
    }
}
