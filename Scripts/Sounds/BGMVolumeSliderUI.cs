using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// BGM‚Ì‰¹—Ê‚ğ’²®‚·‚éƒNƒ‰ƒX
/// </summary>
public class BGMVolumeSliderUI : VolumeSlider
{
    private void Awake()
    {
        SoundManager.Instance.InitialSetBGMSlider(VSlider);
    }

    /// <summary>
    /// “®‚©‚µ‚½‚ç‰¹—Ê‚ğ•Ï‚¦‚é
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        SoundManager.Instance.SetChangeBGMVolume(VSlider);
    }
}
