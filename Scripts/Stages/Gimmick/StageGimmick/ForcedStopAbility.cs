using UnityEngine;

/// <summary>
/// お餅を強制的に停止させるギミックのクラス
/// </summary>
public class ForcedStopAbility : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IRiceCake>(out var _ricacake))
        {
            //お餅を持っていたら強制停止させる
            _ricacake.StopRicecake();
        }
    }
}
