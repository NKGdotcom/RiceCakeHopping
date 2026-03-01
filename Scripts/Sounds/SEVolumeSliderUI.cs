using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SEVolumeSliderUI : VolumeSlider
{
    private void Start()
    {
        SoundManager.Instance.InitialSetSESlider(VSlider);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        SoundManager.Instance.SetChangeSEVolume(VSlider);
    }
}
