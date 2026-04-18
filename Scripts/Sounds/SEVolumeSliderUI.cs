using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// SE‚Ì‰¹—Ê‚ğ’²®‚·‚éƒNƒ‰ƒX
/// </summary>
public class SEVolumeSliderUI : VolumeSlider
{
    private void Start()
    {
        SoundManager.Instance.InitialSetSESlider(VSlider);
    }

    /// <summary>
    /// “®‚©‚µ‚½‚ç‰¹—Ê‚ğ’²®‚·‚é
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        SoundManager.Instance.SetChangeSEVolume(VSlider);
    }
}
