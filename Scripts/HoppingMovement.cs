using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoppingMovement : MonoBehaviour
{
    [SerializeField] private HoppingData hoppingData;
    [SerializeField] private Transform feetShadow;
    [SerializeField] private float shadowHeight = 0.3f;

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
        HoppingMove();

        RaycastHit _hit;
        if(Physics.Raycast(this.transform.position, Vector3.down, out _hit, Mathf.Infinity))
        {
            feetShadow.transform.position = _hit.point + Vector3.up * shadowHeight;
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
    /// 餅に力を加える
    /// </summary>
    /// <param name="_riceCake"></param>
    private void AddForceRiceCake(Collision _riceCake)
    {
        SoundManager.Instance.PlaySE(SESource.riceCakeCollision);
        RicecakeObject _targetRiceCake = _riceCake.gameObject.GetComponent<RicecakeObject>();

        if(_targetRiceCake != null)
        {
            Vector3 _contactPoint = _riceCake.contacts[0].point;
            _contactPoint.y = transform.position.y;
            Vector3 _forceDIrection = (_contactPoint - transform.position).normalized;
            _forceDIrection = new Vector3(_forceDIrection.x, 0, _forceDIrection.z);
            _targetRiceCake.OnHitByHopping(_forceDIrection, hoppingData.RicecakeKnockbackPower);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        HoppingJump();
        if(collision.gameObject.TryGetComponent<RicecakeObject>(out RicecakeObject targetRiceCake))
        {
            AddForceRiceCake(collision);
        }
    }
}
