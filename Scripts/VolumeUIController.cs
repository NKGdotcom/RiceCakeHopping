using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUIController : MonoBehaviour
{
    [SerializeField] private SoundVolume soundVolume; // SoundVolume�ւ̎Q��
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    private void Start()
    {
        if (soundVolume == null)
        {
            Debug.LogError("SoundVolume ScriptableObject��VolumeUIController�Ɋ��蓖�Ă��Ă��܂���B");
            return;
        }

        // PlayerPrefs���特�ʂ����[�h���ASoundVolume�ɐݒ�iSoundManager��Awake�ł��s���邪�AUI�\���̂��߂ɂ����ł����[�h�j
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

        // �������[�h���ꂽ���ʂ�SoundManager�ɓK�p������
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ApplyVolumes();
        }
    }

    public void SetMasterVolume(float value)
    {
        soundVolume.MusterVolume = value;
        soundVolume.SaveToPlayerPrefs(); // PlayerPrefs�ɕۑ�
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ApplyVolumes(); // SoundManager�ɉ��ʕύX��ʒm
        }
    }

    public void SetBGMVolume(float value)
    {
        soundVolume.BGMVolume = value;
        soundVolume.SaveToPlayerPrefs(); // PlayerPrefs�ɕۑ�
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ApplyVolumes(); // SoundManager�ɉ��ʕύX��ʒm
        }
    }

    public void SetSEVolume(float value)
    {
        soundVolume.SEVolume = value;
        soundVolume.SaveToPlayerPrefs(); // PlayerPrefs�ɕۑ�
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UpdateSEVolume(); // SE�̉��ʐݒ���X�V
        }
    }

    private void OnDestroy()
    {
        if (bgmVolumeSlider != null) bgmVolumeSlider.onValueChanged.RemoveListener(SetBGMVolume);
        if (seVolumeSlider != null) seVolumeSlider.onValueChanged.RemoveListener(SetSEVolume);
    }
}