using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicecakeObject : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;

    public StageData.RicecakeType MyType { get => myType; private set => myType = value; }
    public float RicecakeSize { get => ricecakeSize; private set => ricecakeSize = value; }

    private StageData.RicecakeType myType = StageData.RicecakeType.Normal;
    private float ricecakeSize = 1f;
    private float afterRiceCakeSize = 0.7f;
    private float reflectPower = 15f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// ホッピングに触れた時に加わる力
    /// </summary>
    /// <param name="_forceDir"></param>
    /// <param name="_power"></param>
    public void OnHitByHopping(Vector3 _forceDir, float _power)
    {
        if(hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        if(rb != null)
        {
            rb.AddForce(_forceDir * _power, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// Reflectオブジェクトに当たる
    /// </summary>
    /// <param name="_reflectPos"></param>
    public void OnHitByReflect(Vector3 _reflectPos)
    {
        Vector3 _forceDir = (transform.position - _reflectPos).normalized;
        if (rb != null)
        {
            Debug.Log("跳ね返り");
            rb.AddForce(_forceDir * reflectPower, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<RicecakeObject>(out RicecakeObject ricecakeObj))
        {
            StickRicecake(collision);
        }
    }

    /// <summary>
    /// 餅がくっつく
    /// </summary>
    /// <param name="collision"></param>
    private void StickRicecake(Collision collision)
    {
        StopRigidVelocity(rb);
        RicecakeObject otherRicecake = collision.gameObject.GetComponent<RicecakeObject>();
        if (otherRicecake == null) return;
        Vector3 otherRicecakePos = otherRicecake.gameObject.transform.position;
        Vector3 otherRicecakeScale = otherRicecake.transform.localScale;
        if (gameObject.GetInstanceID() > otherRicecake.gameObject.GetInstanceID())
        {
            gameObject.SetActive(false); 
            return;
        }

        //---ここから下は選ばれた片方のみ実行---

        transform.position = (transform.position + otherRicecakePos) / 2;
        Instantiate(hitEffect, transform.position, Quaternion.identity);

        SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);

        ricecakeSize++;
        gameObject.transform.localScale += otherRicecakeScale;
        gameObject.transform.localScale *= afterRiceCakeSize;
    }
    /// <summary>
    /// 味を変える
    /// </summary>
    /// <param name="ricecakeType"></param>
    public void ChangeTaste(StageData.RicecakeType ricecakeType)
    {
        MyType = ricecakeType;
    }
    /// <summary>
    /// 餅のカット
    /// </summary>
    public void CutRiceCake()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        ricecakeSize = ricecakeSize / 2;
        gameObject.transform.localScale *= afterRiceCakeSize;
    }
    /// <summary>
    /// 外部から餅のスピードを止めたい
    /// </summary>
    public void StopRiceCake()
    {
        StopRigidVelocity(rb);
    }
    /// <summary>
    /// 速度を止める
    /// </summary>
    /// <param name="_rb"></param>
    private void StopRigidVelocity(Rigidbody _rb)
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
}
