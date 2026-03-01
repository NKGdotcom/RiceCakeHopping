using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ñ›ÇÃìÆÇ´Çä«óùÇ∑ÇÈÉNÉâÉX
/// è’ìÀîªíËÇ»Ç«Ç‡
/// </summary>
public class RicecakeController : MonoBehaviour, IRiceCake
{
    [SerializeField] private RicecakePhysicsMover physicsMover;
    [SerializeField] private RicecakeChangeScale ricecakeChangeScale;
    [SerializeField] private RicecakeChangeColor ricecakeChangeColor;
    [SerializeField] private RicecakeHitEffect ricecakeHitEffect;

    public RicecakeType MyType { get; private set; }
    public float RicecakeSize { get; private set; } = 1;

    private void Awake()
    {
        if (physicsMover == null) { TryGetComponent<RicecakePhysicsMover>(out physicsMover); }
        if (ricecakeChangeScale == null) { TryGetComponent<RicecakeChangeScale>(out ricecakeChangeScale); }
        if (ricecakeHitEffect == null) { TryGetComponent<RicecakeHitEffect>(out ricecakeHitEffect); }

        RicecakeSize = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IRiceCake>(out var _ricecake))
        {
            MargeRicecake(collision);
            SoundManager.Instance.PlaySE(SESource.riceCakeUnion);
        }
    }

    //ñ›ìØémÇ™Ç‘Ç¬Ç©Ç¡ÇΩÇ∆Ç´åãçá
    public void MargeRicecake(Collision _otherRicecake)
    {
        RicecakeSize++;
        ricecakeHitEffect.ShowHitParticle(MyType);
        StopRicecake();
        ricecakeChangeScale.StickSize(_otherRicecake);
    }
    public void StopRicecake()
    {
        physicsMover.StopForce();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IRicecakeFlavoring>(out var _ricecakeFlavoring))
        {
            ChangeFlavoring(_ricecakeFlavoring);
            SoundManager.Instance.PlaySE(SESource.riceCakeUnion);
        }
    }
    //ñ°ïœçX
    public void ChangeFlavoring(IRicecakeFlavoring _ricecake)
    {
        MyType = _ricecake.MyType;
        StopRicecake();
        ricecakeChangeColor.ChangeRicecakeColor(MyType);
        ricecakeHitEffect.ShowHitParticle(MyType);
    }
    public void CutRicecake()
    {
        RicecakeSize = RicecakeSize / 2;
        ricecakeHitEffect.ShowHitParticle(MyType);
        ricecakeChangeScale.CutSize();
    }
    public void OnHitByHopping(Collision _collision)
    {
        ricecakeHitEffect.ShowHitParticle(MyType);
        physicsMover.AddForceRicecakeCol(_collision);
    }
    public void OnHitByReflect(Vector3 _reflectPos)
    {
        physicsMover.AddForceReflect(_reflectPos);
    }
}
