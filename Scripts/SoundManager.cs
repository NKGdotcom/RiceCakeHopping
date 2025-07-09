using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;

    [SerializeField] private SoundList soundList;
    [SerializeField] private SoundVolume soundVolume; // ��SoundVolume�ւ̎Q�Ƃ�ǉ�

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
            Debug.LogError("SoundVolume ScriptableObject��SoundManager�Ɋ��蓖�Ă��Ă��܂���B");
        }
    }

    // BGM�̉��ʂ��X�V���郁�\�b�h
    public void UpdateBGMVolume()
    {
        if (soundVolume != null && bgmAudioSource != null)
        {
            bgmAudioSource.volume = soundVolume.MusterVolume * soundVolume.BGMVolume;
            Debug.Log($"BGM Volume Updated: {bgmAudioSource.volume}");
        }
    }

    // SE�̉��ʂ��X�V���郁�\�b�h (PlayOneShot�̐�����A�������f�͂��ꂸ�A����Đ����ɓK�p)
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
            UpdateBGMVolume(); // �Đ��O�ɉ��ʂ�K�p
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
            Debug.Log($"Playing BGM: {_bgmSource} with volume: {bgmAudioSource.volume}");
        }
        else
        {
            Debug.LogWarning($"BGM {_bgmSource} ��������܂���");
        }
    }

    public void PlaySE(SESource _seSource)
    {
        SoundList.SESoundData seData = soundList.GetSEData(_seSource);
        if (seData != null && seData.SEAudioClip != null)
        {
            // SE��PlayOneShot�ŉ��ʂ�ݒ�
            float seFinalVolume = soundVolume.MusterVolume * soundVolume.SEVolume;
            seAudioSource.PlayOneShot(seData.SEAudioClip, seFinalVolume);
            Debug.Log($"Playing SE: {_seSource} with volume: {seFinalVolume}");
        }
        else
        {
            Debug.LogWarning($"SE {_seSource} ��������܂���");
        }
    }
}