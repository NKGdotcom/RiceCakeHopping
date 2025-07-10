using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �݂������ɐG�ꂽ�Ƃ��̏������e
/// </summary>
public class RiceCakeManager : MonoBehaviour
{
    private List<GameObject> stageRiceCakeList = new List<GameObject>(); //�X�e�[�W���̖�
    [SerializeField] private GameObject collisionParticle; //�݂����̂����Ƃ��̃p�[�e�B�N�� 

    [SerializeField] private float afterRiceCakeSize = 0.7f;
    [SerializeField] private float cutRiceCakeSize = 0.5f;

    public float RiceCakeSize { get; private set; } //���݂̑傫��

    private void Start()
    {
        stageRiceCakeList.AddRange(GameObject.FindGameObjectsWithTag("Normal"));
        RiceCakeSize = 1;
    }

    /// <summary>
    /// �ݓ��m���G�ꂽ�Ƃ��ɖ݂̑傫�����傫���Ȃ�
    /// </summary>
    /// <param name="oneRiceCake">�����</param>
    /// <param name="oneMoreRiceCake">������̖�</param>
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

            GameObject _removeRiceCake = _oneRiceCake; //��������݂�
            GameObject _growRiceCake = _oneMoreRiceCake; //�������傫������݂�

            Vector3 _removeRiceCakeScale = _removeRiceCake.transform.localScale;
            _growRiceCake.transform.localScale += _removeRiceCakeScale;
            _growRiceCake.transform.localScale *= afterRiceCakeSize; //�T�C�Y��傫������

            stageRiceCakeList.Remove(_removeRiceCake);
            RiceCakeSize ++;

            _removeRiceCake.SetActive(false);
            SoundManager.Instance.PlaySE(SESource.riceCakeUnionAndButton);
        }
    }
    /// <summary>
    /// �݂���ɐG�ꂽ�Ƃ�
    /// </summary>
    /// <param name="_riceCake"></param>
    public void CutRiceCake(GameObject _riceCake) 
    {
        Rigidbody _riceCakeRb = _riceCake.GetComponent<Rigidbody>();

        Vector3 _particleInstantiatePosition = _riceCake.transform.position;

        Instantiate(collisionParticle, _particleInstantiatePosition, Quaternion.identity);
        
        RiceCakeSize -= cutRiceCakeSize;
        _riceCake.transform.localScale *= afterRiceCakeSize; //����������

        SoundManager.Instance.PlaySE(SESource.cut);
    }
    /// <summary>
    /// ���x���~�߂�
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
    /// �X�e�[�W��̃I�u�W�F�N�g������(���U���g���o�p)
    /// </summary>
    public void GameObjectFalse()
    {
        for(int i =0; i < stageRiceCakeList.Count; i++)
        {
            stageRiceCakeList[i].SetActive(false);
        }
    }
}
