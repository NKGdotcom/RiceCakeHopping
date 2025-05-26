using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceCakeManager : MonoBehaviour
{
    public float RiceCakeSize { get => riceCakeSize; set => riceCakeSize = value; }

    [SerializeField] private List<GameObject> stageRiceCakeList = new List<GameObject>();
    [SerializeField] private GameObject collisionParticle; //–İ‚ª‡‘Ì‚µ‚½‚Æ‚«‚Ìƒp[ƒeƒBƒNƒ‹ 
    [SerializeField] private StageManager stageManager;

    [SerializeField] private float afterRiceCakeSize = 0.7f;
    [SerializeField] private float cutRiceCakeSize = 0.5f;

    private float riceCakeSize;  //‚¨–İ‚Ì‘å‚«‚³

    private void Start()
    {
        riceCakeSize = 1;
    }
    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// –İ“¯m‚ªG‚ê‚½‚Æ‚«‚É–İ‚Ì‘å‚«‚³‚ª‘å‚«‚­‚È‚é
    /// </summary>
    /// <param name="oneRiceCake">‚ ‚é–İ</param>
    /// <param name="oneMoreRiceCake">‚à‚¤ˆê‚Â‚Ì–İ</param>
    public void OnRiceCakeCollision(GameObject _oneRiceCake,GameObject _oneMoreRiceCake)
    {
        if(stageRiceCakeList.Contains(_oneRiceCake) && stageRiceCakeList.Contains(_oneMoreRiceCake))
        {
            Rigidbody _oneRb = _oneRiceCake.GetComponent<Rigidbody>();
            Rigidbody _oneMoreRb = _oneMoreRiceCake.GetComponent<Rigidbody>();
            if(_oneRb != null)
            {
                _oneRb.velocity = Vector3.zero;
                _oneRb.angularVelocity = Vector3.zero; //‰ñ“]‚Ì•ûŒü‚à~‚ß‚é
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
    /// –İ‚ª•ï’š‚ÉG‚ê‚½‚Æ‚«
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
