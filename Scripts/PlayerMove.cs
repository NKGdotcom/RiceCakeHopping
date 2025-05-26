using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private StageManager stageManager;

    [SerializeField] private float rotationDamping = 20; //���l��傫������Ɖ�]�����炩�ɂȂ�

    private float currentXRot; //���݂�x�̉�]��
    private float currentZRot; //���݂�z�̉�]��

    [SerializeField] private GameObject collisionRiceCakeEffect; //�݂ɐG�ꂽ�Ƃ��̃p�[�e�B�N��
    [SerializeField] private GameObject collisionKinakoRiceCakeEffect; //���ȕ�"
    [SerializeField] private GameObject collisionSoySourceRiceCakeEffect; //�ݖ�"

    private Rigidbody hoppingRb;

    // Start is called before the first frame update
    void Start()
    {
        hoppingRb = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!stageManager.IsPause)//�|�[�Y��Ԃ���Ȃ���
        {
            HoppingMove();
            hoppingRb.isKinematic = false;
        }
        else //�|�[�Y��ʂ̎�
        {
            hoppingRb.isKinematic = true;
        }
    }

    /// <summary>
    /// �z�b�s���O�̈ړ�
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

        if (collision.gameObject.CompareTag("RiceCake")||�@//�݂ɐG�ꂽ�Ƃ�
            collision.gameObject.CompareTag("KinakoRiceCake")||
            collision.gameObject.CompareTag("SoySourceRiceCake"))
        {
            Vector3 contactPoint = collision.contacts[0].point; //�ڐG�_
            Vector3 direction = (contactPoint - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);

            Rigidbody riceCakeRb = collision.gameObject.GetComponent<Rigidbody>();
            if(riceCakeRb != null)
            {
                riceCakeRb.AddForce(direction * playerData.RicaCakeKnockbackPower, ForceMode.Impulse);
            }

            //�p�[�e�B�N���𐶐�
            Vector3 particleInstantiatePosition = collision.gameObject.transform.position;
            if (collision.gameObject.CompareTag("RiceCake"))
            {
                Instantiate(collisionRiceCakeEffect, particleInstantiatePosition, Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("KinakoRiceCake"))�@//���ȕ����p�̃p�[�e�B�N��
            {
                Instantiate(collisionKinakoRiceCakeEffect, particleInstantiatePosition, Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("SoySourceRiceCake")) //�ݖ����p�̃p�[�e�B�N��
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
