using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private StageManager stageManager;

    [SerializeField] private float rotationDamping = 20; //数値を大きくすると回転が滑らかになる

    private float currentXRot; //現在のxの回転量
    private float currentZRot; //現在のzの回転量

    [SerializeField] private GameObject collisionRiceCakeEffect; //餅に触れたときのパーティクル
    [SerializeField] private GameObject collisionKinakoRiceCakeEffect; //黄な粉"
    [SerializeField] private GameObject collisionSoySourceRiceCakeEffect; //醤油"

    private Rigidbody hoppingRb;

    // Start is called before the first frame update
    void Start()
    {
        hoppingRb = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!stageManager.IsPause)//ポーズ状態じゃない時
        {
            HoppingMove();
            hoppingRb.isKinematic = false;
        }
        else //ポーズ画面の時
        {
            hoppingRb.isKinematic = true;
        }
    }

    /// <summary>
    /// ホッピングの移動
    /// </summary>
    private void HoppingMove()
    {
        float targetXRot = Input.GetAxis("Vertical") * playerData.RotationSpeed;
        float targetZRot = -Input.GetAxis("Horizontal") * playerData.RotationSpeed;

        currentXRot = Mathf.Lerp(currentXRot, targetXRot, Time.deltaTime * rotationDamping);
        currentZRot = Mathf.Lerp(currentZRot, targetZRot, Time.deltaTime * rotationDamping);

        transform.Rotate(new Vector3(currentXRot, 0, currentZRot) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 jumpDirection = transform.up;
        hoppingRb.velocity = jumpDirection * playerData.BouncePower;

        if (collision.gameObject.CompareTag("RiceCake")||　//餅に触れたとき
            collision.gameObject.CompareTag("KinakoRiceCake")||
            collision.gameObject.CompareTag("SoySourceRiceCake"))
        {
            Vector3 contactPoint = collision.contacts[0].point; //接触点
            Vector3 direction = (contactPoint - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);

            Rigidbody riceCakeRb = collision.gameObject.GetComponent<Rigidbody>();
            if(riceCakeRb != null)
            {
                riceCakeRb.AddForce(direction * playerData.RicaCakeKnockbackPower, ForceMode.Impulse);
            }

            //パーティクルを生成
            Vector3 particleInstantiatePosition = collision.gameObject.transform.position;
            if (collision.gameObject.CompareTag("RiceCake"))
            {
                Instantiate(collisionRiceCakeEffect, particleInstantiatePosition, Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("KinakoRiceCake"))　//黄な粉味用のパーティクル
            {
                Instantiate(collisionKinakoRiceCakeEffect, particleInstantiatePosition, Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("SoySourceRiceCake")) //醤油味用のパーティクル
            {
                Instantiate(collisionSoySourceRiceCakeEffect, particleInstantiatePosition, Quaternion.identity);
            }
            SoundManager.Instance.PlaySE(SESource.riceCakeCollision);
        }
        else
        {
            SoundManager.Instance.PlaySE(SESource.hoppingMove);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
