using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オプション画面などの音量スライダーを制御し、SoundManagerに値を反映させるUIコントローラー
/// </summary>
public class VolumeUIController : MonoBehaviour
{
    [Header("音量スライダー")]
    [Tooltip("BGMの音量を操作するスライダー")]
    [SerializeField] private Slider bgmVolumeSlider;
    [Tooltip("SEの音量を操作するスライダー")]
    [SerializeField] private Slider seVolumeSlider;


    private void Awake()
    {
        if(bgmVolumeSlider == null) { Debug.LogError("bgmVolumeSliderが参照されていません"); return; }
        if(seVolumeSlider == null) { Debug.LogError("seVolumeSliderが参照されていません"); return; }

        
    }
    private void OnEnable()
    {
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        seVolumeSlider.onValueChanged.AddListener(SetSEVolume);
    }

    private void OnDestroy()
    {
        bgmVolumeSlider.onValueChanged.RemoveListener(SetBGMVolume);
        seVolumeSlider.onValueChanged.RemoveListener(SetSEVolume);
    }

    /// <summary>
    /// BGMの音量を調整する
    /// </summary>
    /// <param name="value"></param>
    public void SetBGMVolume(float value)
    {

    }

    /// <summary>
    /// SEの音量を調整する
    /// </summary>
    /// <param name="value"></param>
    public void SetSEVolume(float value)
    {

    }


}