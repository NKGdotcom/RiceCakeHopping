using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoppingMovement : MonoBehaviour
{
    [Header("プレイヤーのパラメーター")]
    [SerializeField] private HoppingData hoppingData;
    [Header("より気持ちよさを演出するためのエフェクト")]
    [SerializeField] private GameObject riceCakeEffect;
    [SerializeField] private GameObject kinakoRiceCakeEffect;
    [SerializeField] private GameObject soySourceRiceCakeEffect;

    private float currentXRot;
    private float currentZRot;

    private Rigidbody hoppingRb;
    // Start is called before the first frame update
    void Start()
    {
        hoppingRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateMachine.Instance.IsPlaying() || GameStateMachine.Instance.IsIntroduceClear())
        {
            HoppingMove(); //ゲームプレイ中か
            hoppingRb.isKinematic = false;
        }
        else
        {
            hoppingRb.isKinematic = true;
        }
    }

    /// <summary>
    /// ホッピングの傾きを変える
    /// </summary>
    private void HoppingMove()
    {
        float _targetXRot = Input.GetAxis("Vertical") * hoppingData.RotationSpeed;
        float _targetZRot = -Input.GetAxis("Horizontal") * hoppingData.RotationSpeed;

        currentXRot = Mathf.Lerp(currentXRot, _targetXRot, Time.deltaTime * hoppingData.SmoothRotation);
        currentZRot = Mathf.Lerp(currentZRot, _targetZRot, Time.deltaTime * hoppingData.SmoothRotation);

        transform.Rotate(new Vector3(currentXRot, 0, currentZRot) * Time.deltaTime);
    }
    /// <summary>
    /// ホッピングのジャンプを再現
    /// </summary>
    private void HoppingJump()
    {
        SoundManager.Instance.PlaySE(SESource.hoppingMove);
        Vector3 _jumpDirection = transform.up;
        hoppingRb.velocity = _jumpDirection * hoppingData.BouncePower;     
    }
    /// <summary>
    /// 餅に触れたら力を加える
    /// </summary>
    /// <param name="_riceCake">触れた餅</param>
    private void AddForceRiceCake(Collision _riceCake)
    {
        //餅に触れたら
        if (_riceCake.gameObject.CompareTag("Normal") ||
           _riceCake.gameObject.CompareTag("KinakoRiceCake") ||
           _riceCake.gameObject.CompareTag("SoySourceRiceCake"))
        {
            SoundManager.Instance.PlaySE(SESource.riceCakeCollision);

            Vector3 _contactPoint = _riceCake.contacts[0].point; //接触点
            Vector3 _forceDirection = (_contactPoint - transform.position).normalized; //力を与える方向
            _forceDirection = new Vector3(_forceDirection.x, 0, _forceDirection.z);

            Rigidbody _riceCakeRb = _riceCake.gameObject.GetComponent<Rigidbody>();
            if (_riceCakeRb != null) _riceCakeRb.AddForce(_forceDirection * hoppingData.RiceCakeKnockbackPower, ForceMode.Impulse); //力を加える

            Vector3 _particleInstantiatePos = _riceCake.gameObject.transform.position;

            if (_riceCake.gameObject.CompareTag("Normal")) Instantiate(riceCakeEffect, _particleInstantiatePos, Quaternion.identity);                   //プレーン味
            if (_riceCake.gameObject.CompareTag("KinakoRiceCake")) Instantiate(kinakoRiceCakeEffect, _particleInstantiatePos, Quaternion.identity);       //黄な粉味
            if (_riceCake.gameObject.CompareTag("SoySourceRiceCake")) Instantiate(soySourceRiceCakeEffect, _particleInstantiatePos, Quaternion.identity); //醤油味
        }
    }
    private void OnCollisionEnter(Collision _collision)
    {
        HoppingJump();
        AddForceRiceCake(_collision);
    }
}
