using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餅の物理挙動を管理するクラス
/// </summary>
public class RicecakePhysicsMover : MonoBehaviour
{
    //コンポーネント
    private Rigidbody ricecakeRb;

    //パラメータ
    //餅を飛ばす際に加わる力
    private float ricecakeForcePower = 20f;
    //反射したときの力
    private float reflectPower = 15f;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Rigidbody>(out ricecakeRb);
    }

    /// <summary>
    /// ホッピングに触れたら餅に力を加える
    /// </summary>
    /// <param name="_collision"></param>
    public void AddForceRicecakeCol(Collision _collision)
    {
        if (ricecakeRb == null) return;

        //触れた位置をピンポイントで取得
        Vector3 _contactPoint = _collision.contacts[0].point;
        //飛ばす方向を決める
        Vector3 _forceDir = (_collision.transform.position - _contactPoint).normalized;

        //飛ばす方向をy軸は無視する
        _forceDir = new Vector3(_forceDir.x, 0, _forceDir.z).normalized;
        ricecakeRb.AddForce(_forceDir * ricecakeForcePower, ForceMode.Impulse);
    }

    /// <summary>
    /// 反射版に触れたとら餅に跳ね返りの力を加える
    /// </summary>
    /// <param name="_reflectPos"></param>
    public void AddForceReflect(Vector3 _reflectPos)
    {
        Vector3 _forceDir = (transform.position - _reflectPos).normalized;

        ricecakeRb.AddForce(_forceDir * reflectPower, ForceMode.Impulse);
    }

    /// <summary>
    /// 力を強制的に止める
    /// </summary>
    public void StopForce()
    {
        if(ricecakeRb == null) return;

        //速度、加速度を止める
        ricecakeRb.velocity = Vector3.zero;
        ricecakeRb.angularVelocity = Vector3.zero;
    }
}
