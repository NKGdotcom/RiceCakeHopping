using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BGMVolumeSliderUI : VolumeSlider
{
    private void Start()
    {
        SoundManager.Instance.InitialSetBGMSlider(VSlider);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        SoundManager.Instance.SetChangeBGMVolume(VSlider);
    }
}
