using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour, IPointerUpHandler
{
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
