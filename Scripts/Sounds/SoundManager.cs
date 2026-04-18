using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// シングルトンで音を鳴らす役割を持つもの
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("音関連")]
    [Tooltip("BGMを鳴らすためのオーディオソース")]
    [SerializeField] private AudioSource bgmAudioSource;
    [Tooltip("SEを鳴らすためのオーディオソース")]
    [SerializeField] private AudioSource seAudioSource;
    [Tooltip("音がデータとして保存してあるリスト")]
    [SerializeField] private SoundList soundList;
    [Tooltip("音量調節をするクラス")]
    [SerializeField] private SoundVolume soundVolume;


    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("SoundManager instance = null. Setting up.");
            Instance = this;
        }
        else
        {
            Debug.LogError("SoundVolume ScriptableObjectがSoundManagerに割り当てられていません。");
        }
    }

    /// <summary>
    /// BGMを鳴らす
    /// </summary>
    /// <param name="_bgmSource"></param>
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
            Debug.LogWarning($"BGM {_bgmSource} が見つかりません");
        }
    }
    /// <summary>
    /// BGMの音量調節
    /// </summary>
    public void SetChangeBGMVolume(Slider _slider)
    {
        soundVolume.BGMVolume = _slider.value;
        bgmAudioSource.volume = soundVolume.BGMVolume;
    }
    /// <summary>
    /// SEの音量調節
    /// </summary>
    public void SetChangeSEVolume(Slider _slider)
    {
        soundVolume.SEVolume = _slider.value;
        seAudioSource.volume = soundVolume.SEVolume;
    }

    /// <summary>
    /// BGMの初期値を設定
    /// </summary>
    /// <param name="_slider"></param>
    public void InitialSetBGMSlider(Slider _slider)
    {
        _slider.value = soundVolume.BGMVolume;
    }

    /// <summary>
    /// SEの初期値を設定
    /// </summary>
    /// <param name="_slider"></param>
    public void InitialSetSESlider(Slider _slider)
    {
        _slider.value = soundVolume.SEVolume;
    }

    /// <summary>
    /// SEを流す
    /// </summary>
    /// <param name="_seSource"></param>
    public void PlaySE(SESource _seSource)
    {
        SoundList.SESoundData seData = soundList.GetSEData(_seSource);
        if (seData != null && seData.SEAudioClip != null)
        {
            seAudioSource.PlayOneShot(seData.SEAudioClip);
        }
        else
        {
            Debug.LogWarning($"SE {_seSource} が見つかりません");
        }
    }
}