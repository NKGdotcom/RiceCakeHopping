using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource seAudioSource;

    [SerializeField] private SoundList soundList;

    [SerializeField] private float masterVolume = 1;
    [SerializeField] private float bgmMasterVolume = 1;
    [SerializeField] private float seMasterVolume = 1;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) // Unity風nullチェック
        {
            Debug.Log("instance = null");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayBGM(BGMSource bgmSource)
    {
        SoundList.BGMSoundData bgmData = soundList.GetBGMData(bgmSource);
        if (bgmData != null && bgmData.BGMAudioClip != null)
        {
            bgmAudioSource.clip = bgmData.BGMAudioClip;
            bgmAudioSource.volume = masterVolume * bgmMasterVolume;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM {bgmSource} が見つかりません");
        }
    }

    public void PlaySE(SESource seSource)
    {
        SoundList.SESoundData seData = soundList.GetSEData(seSource);
        if (seData != null && seData.SEAudioClip != null)
        {
            seAudioSource.PlayOneShot(seData.SEAudioClip, masterVolume * seMasterVolume);
        }
        else
        {
            Debug.LogWarning($"SE {seSource} が見つかりません");
        }
    }
}