using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;

    [SerializeField] private SoundList soundList;
    [SerializeField] private SoundVolume soundVolume; // ★SoundVolumeへの参照を追加

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("SoundManager instance = null. Setting up.");
            Instance = this;
        }
        if (soundVolume != null)
        {
            soundVolume.LoadFromPlayerPrefs();
            ApplyVolumes();
        }
        else
        {
            Debug.LogError("SoundVolume ScriptableObjectがSoundManagerに割り当てられていません。");
        }
    }

    // BGMの音量を更新するメソッド
    public void UpdateBGMVolume()
    {
        if (soundVolume != null && bgmAudioSource != null)
        {
            bgmAudioSource.volume = soundVolume.MusterVolume * soundVolume.BGMVolume;
            Debug.Log($"BGM Volume Updated: {bgmAudioSource.volume}");
        }
    }

    // SEの音量を更新するメソッド (PlayOneShotの性質上、即時反映はされず、次回再生時に適用)
    public void UpdateSEVolume()
    {
        Debug.Log($"SE Volume setting updated to: {soundVolume.MusterVolume * soundVolume.SEVolume}");
    }

    public void ApplyVolumes()
    {
        UpdateBGMVolume();
        UpdateSEVolume();
    }

    public void PlayBGM(BGMSource _bgmSource)
    {
        SoundList.BGMSoundData bgmData = soundList.GetBGMData(_bgmSource);
        if (bgmData != null && bgmData.BGMAudioClip != null)
        {
            bgmAudioSource.clip = bgmData.BGMAudioClip;
            UpdateBGMVolume(); // 再生前に音量を適用
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
            Debug.Log($"Playing BGM: {_bgmSource} with volume: {bgmAudioSource.volume}");
        }
        else
        {
            Debug.LogWarning($"BGM {_bgmSource} が見つかりません");
        }
    }

    public void PlaySE(SESource _seSource)
    {
        SoundList.SESoundData seData = soundList.GetSEData(_seSource);
        if (seData != null && seData.SEAudioClip != null)
        {
            // SEはPlayOneShotで音量を設定
            float seFinalVolume = soundVolume.MusterVolume * soundVolume.SEVolume;
            seAudioSource.PlayOneShot(seData.SEAudioClip, seFinalVolume);
            Debug.Log($"Playing SE: {_seSource} with volume: {seFinalVolume}");
        }
        else
        {
            Debug.LogWarning($"SE {_seSource} が見つかりません");
        }
    }
}