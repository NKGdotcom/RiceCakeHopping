using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;
    [SerializeField] private SoundList soundList;
    [SerializeField] private SoundVolume soundVolume;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("SoundManager instance = null. Setting up.");
            Instance = this;
        }
        else
        {
            Debug.LogError("SoundVolume ScriptableObjectÇ™SoundManagerÇ…äÑÇËìñÇƒÇÁÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
        }
    }
    public void PlayBGM(BGMSource _bgmSource)
    {
        SoundList.BGMSoundData bgmData = soundList.GetBGMData(_bgmSource);
        if (bgmData != null && bgmData.BGMAudioClip != null)
        {
            bgmAudioSource.clip = bgmData.BGMAudioClip;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
            Debug.Log($"Playing BGM: {_bgmSource} with volume: {bgmAudioSource.volume}");
        }
        else
        {
            Debug.LogWarning($"BGM {_bgmSource} Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
        }
    }
    /// <summary>
    /// BGMÇÃâπó í≤êﬂ
    /// </summary>
    public void SetChangeBGMVolume(Slider _slider)
    {
        soundVolume.BGMVolume = _slider.value;
        bgmAudioSource.volume = soundVolume.BGMVolume;
    }
    /// <summary>
    /// SEÇÃâπó í≤êﬂ
    /// </summary>
    public void SetChangeSEVolume(Slider _slider)
    {
        soundVolume.SEVolume = _slider.value;
        seAudioSource.volume = soundVolume.SEVolume;
    }

    public void InitialSetBGMSlider(Slider _slider)
    {
        _slider.value = soundVolume.BGMVolume;
    }

    public void InitialSetSESlider(Slider _slider)
    {
        _slider.value = soundVolume.SEVolume;
    }

    public void PlaySE(SESource _seSource)
    {
        SoundList.SESoundData seData = soundList.GetSEData(_seSource);
        if (seData != null && seData.SEAudioClip != null)
        {
            seAudioSource.PlayOneShot(seData.SEAudioClip);
        }
        else
        {
            Debug.LogWarning($"SE {_seSource} Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
        }
    }
}