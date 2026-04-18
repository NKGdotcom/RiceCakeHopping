using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音をデータとして保存
/// </summary>
[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundData")]
public class SoundList : ScriptableObject
{
    /// <summary>
    /// BGMとして保存
    /// </summary>
    [System.Serializable]
    public class BGMSoundData
    {
        [Header("BGM")]
        [Tooltip("音の種類")]
        public BGMSource BGMSource;
        [Tooltip("音のクリップ")]
        public AudioClip BGMAudioClip;
        public BGMSoundData(BGMSource BGMSource, AudioClip BGMAudioClip)
        {
            this.BGMSource = BGMSource;
            this.BGMAudioClip = BGMAudioClip;
        }
    }

    /// <summary>
    /// SEとして保存
    /// </summary>
    [System.Serializable]
    public class SESoundData
    {
        [Header("SE")]
        [Tooltip("音の種類")]
        public SESource SESource;
        [Tooltip("音のクリップ")]
        public AudioClip SEAudioClip;
        public SESoundData(SESource SESource, AudioClip SEAudioClip)
        {
            this.SESource = SESource;
            this.SEAudioClip = SEAudioClip;
        }
    }
    public List<BGMSoundData> bgmSoundDataList = new List<BGMSoundData>();
    public List<SESoundData> seSoundDataList = new List<SESoundData>();

    /// <summary>
    /// BGMのデータを取得
    /// </summary>
    /// <param name="BGMSource"></param>
    /// <returns></returns>
    public BGMSoundData GetBGMData(BGMSource BGMSource)
    {
        foreach (BGMSoundData bgm in bgmSoundDataList)
        {
            if (bgm.BGMSource == BGMSource)
            {
                return new BGMSoundData(bgm.BGMSource, bgm.BGMAudioClip);
            }
        }
        return null;
    }

    /// <summary>
    /// SEのデータを取得
    /// </summary>
    /// <param name="SESource"></param>
    /// <returns></returns>
    public SESoundData GetSEData(SESource SESource)
    {
        foreach (SESoundData se in seSoundDataList)
        {
            if (se.SESource == SESource)
            {
                return new SESoundData(se.SESource, se.SEAudioClip);
            }
        }
        return null;
    }
}
