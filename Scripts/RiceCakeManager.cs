using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCakeManager : MonoBehaviour
{
    public float RiceCakeSize { get => riceCakeSize; set => riceCakeSize = value; }

    [SerializeField] private List<GameObject> stageRiceCakeList = new List<GameObject>();
    [SerializeField] private GameObject collisionParticle; //餅が合体したときのパーティクル 
    [SerializeField] private StageManager stageManager;

    [SerializeField] private float afterRiceCakeSize = 0.7f;
    [SerializeField] private float cutRiceCakeSize = 0.5f;

    private float riceCakeSize;  //お餅の大きさ

    private void Start()
    {
        riceCakeSize = 1;
    }
    // Update is called once per frame
    void Update()
    {

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
            if(_oneRb != null)
            {
                _oneRb.velocity = Vector3.zero;
                _oneRb.angularVelocity = Vector3.zero; //回転の方向も止める
            }
            if(_oneMoreRb != null)
            {
                _oneMoreRb.velocity = Vector3.zero;
                _oneMoreRb.angularVelocity = Vector3.zero;
            }

            Vector3 _particleInstantiatePosition = (_oneRiceCake.transform.position + _oneMoreRiceCake.transform.position) / 2;

            if(collisionParticle != null)
            {
                Instantiate(collisionParticle, _particleInstantiatePosition, Quaternion.identity);
            }

            GameObject _removeRiceCake = _oneRiceCake;
            GameObject _growRiceCake = _oneMoreRiceCake;

            Vector3 _removeRiceCakeScale = _removeRiceCake.transform.localScale;
            _growRiceCake.transform.localScale += _removeRiceCakeScale;
            _growRiceCake.transform.localScale *= afterRiceCakeSize;

            stageRiceCakeList.Remove(_removeRiceCake);
            riceCakeSize ++;

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
        
        riceCakeSize -= cutRiceCakeSize;
        _riceCake.transform.localScale *= afterRiceCakeSize;

        SoundManager.Instance.PlaySE(SESource.cut);
    }
}
