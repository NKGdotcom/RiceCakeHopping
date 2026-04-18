using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 音量の調節スライダーのベースとなるクラス
/// </summary>
public class VolumeSlider : MonoBehaviour, IPointerUpHandler
{
    /// <summary>
    /// スライダーの調節
    /// </summary>
    public Slider VSlider { get => volumeSlider; private set => volumeSlider = value; }
    [SerializeField] private Slider volumeSlider;
    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }
    public void InitialSetSlider(float _value)
    {
        volumeSlider.value = _value;
    }
}
