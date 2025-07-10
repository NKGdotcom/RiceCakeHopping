using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅が何かに触れたときの処理内容
/// </summary>
public class RiceCakeManager : MonoBehaviour
{
    private List<GameObject> stageRiceCakeList = new List<GameObject>(); //ステージ内の餅
    [SerializeField] private GameObject collisionParticle; //餅が合体したときのパーティクル 

    [SerializeField] private float afterRiceCakeSize = 0.7f;
    [SerializeField] private float cutRiceCakeSize = 0.5f;

    public float RiceCakeSize { get; private set; } //お餅の大きさ

    private void Start()
    {
        stageRiceCakeList.AddRange(GameObject.FindGameObjectsWithTag("Normal"));
        RiceCakeSize = 1;
    }

    /// <summary>
    /// 餅同士が触れたときに餅の大きさが大きくなる
    /// </summary>
    /// <param name="oneRiceCake">ある餅</param>
    /// <param name="oneMoreRiceCake">もう一つの餅</param>
    public void OnRiceCakeCollision(GameObject _oneRiceCake,GameObject _oneMoreRiceCake)
    {
        if(stageRiceCakeList.Contains(_oneRiceCake) && stageRiceCakeList.Contains(_oneMoreRiceCake))
        {
            Rigidbody _oneRb = _oneRiceCake.GetComponent<Rigidbody>();
            Rigidbody _oneMoreRb = _oneMoreRiceCake.GetComponent<Rigidbody>();
            StopRigidVelocity(_oneRb);
            StopRigidVelocity(_oneMoreRb);

            Vector3 _particleInstantiatePosition = (_oneRiceCake.transform.position + _oneMoreRiceCake.transform.position) / 2;
            if(collisionParticle != null) Instantiate(collisionParticle, _particleInstantiatePosition, Quaternion.identity);

            GameObject _removeRiceCake = _oneRiceCake; //一つを消す餅に
            GameObject _growRiceCake = _oneMoreRiceCake; //もう一つを大きくする餅に

            Vector3 _removeRiceCakeScale = _removeRiceCake.transform.localScale;
            _growRiceCake.transform.localScale += _removeRiceCakeScale;
            _growRiceCake.transform.localScale *= afterRiceCakeSize; //サイズを大きくする

            stageRiceCakeList.Remove(_removeRiceCake);
            RiceCakeSize ++;

            _removeRiceCake.SetActive(false);
            SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        }
    }
    /// <summary>
    /// 餅が包丁に触れたとき
    /// </summary>
    /// <param name="_riceCake"></param>
    public void CutRiceCake(GameObject _riceCake) 
    {
        Rigidbody _riceCakeRb = _riceCake.GetComponent<Rigidbody>();

        Vector3 _particleInstantiatePosition = _riceCake.transform.position;

        Instantiate(collisionParticle, _particleInstantiatePosition, Quaternion.identity);
        
        RiceCakeSize -= cutRiceCakeSize;
        _riceCake.transform.localScale *= afterRiceCakeSize; //小さくする

        SoundManager.Instance.PlaySE(SESource.cut);
    }
    /// <summary>
    /// 速度を止める
    /// </summary>
    /// <param name="_rb"></param>
    private void StopRigidVelocity(Rigidbody _rb)
    {
        if(_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
    /// <summary>
    /// ステージ上のオブジェクトを消す(リザルト演出用)
    /// </summary>
    public void GameObjectFalse()
    {
        for(int i =0; i < stageRiceCakeList.Count; i++)
        {
            stageRiceCakeList[i].SetActive(false);
        }
    }
}
