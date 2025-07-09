using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUIController : MonoBehaviour
{
    [SerializeField] private SoundVolume soundVolume; // SoundVolumeへの参照
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    private void Start()
    {
        if (soundVolume == null)
        {
            Debug.LogError("SoundVolume ScriptableObjectがVolumeUIControllerに割り当てられていません。");
            return;
        }

        // PlayerPrefsから音量をロードし、SoundVolumeに設定（SoundManagerのAwakeでも行われるが、UI表示のためにここでもロード）
        soundVolume.LoadFromPlayerPrefs();


        if (bgmVolumeSlider != null)
        {
            bgmVolumeSlider.value = soundVolume.BGMVolume;
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }
        if (seVolumeSlider != null)
        {
            seVolumeSlider.value = soundVolume.SEVolume;
            seVolumeSlider.onValueChanged.AddListener(SetSEVolume);
        }

        // 初期ロードされた音量をSoundManagerに適用させる
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ApplyVolumes();
        }
    }

    public void SetMasterVolume(float value)
    {
        soundVolume.MusterVolume = value;
        soundVolume.SaveToPlayerPrefs(); // PlayerPrefsに保存
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ApplyVolumes(); // SoundManagerに音量変更を通知
        }
    }

    public void SetBGMVolume(float value)
    {
        soundVolume.BGMVolume = value;
        soundVolume.SaveToPlayerPrefs(); // PlayerPrefsに保存
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ApplyVolumes(); // SoundManagerに音量変更を通知
        }
    }

    public void SetSEVolume(float value)
    {
        soundVolume.SEVolume = value;
        soundVolume.SaveToPlayerPrefs(); // PlayerPrefsに保存
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UpdateSEVolume(); // SEの音量設定を更新
        }
    }

    private void OnDestroy()
    {
        if (bgmVolumeSlider != null) bgmVolumeSlider.onValueChanged.RemoveListener(SetBGMVolume);
        if (seVolumeSlider != null) seVolumeSlider.onValueChanged.RemoveListener(SetSEVolume);
    }
}