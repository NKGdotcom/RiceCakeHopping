using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundVolume", menuName = "ScriptableObjects/Sound/SoundVolume")]
public class SoundVolume : ScriptableObject//どこからでも音の高さを変えられるように
{
    public float MusterVolume { get => musterVolume; set => musterVolume = value; }
    public float BGMVolume { get => bgmVolume; set => bgmVolume = value; }
    public float SEVolume { get => seVolume; set => seVolume = value; }

    [SerializeField][Range(0, 1)] private float musterVolume;
    [SerializeField][Range(0,1)] private float bgmVolume;
    [SerializeField][Range(0, 1)] private float seVolume;

    public void LoadFromPlayerPrefs()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", BGMVolume);
        seVolume = PlayerPrefs.GetFloat("SEVolume", SEVolume);
    }

    // PlayerPrefsに現在の値を保存するためのメソッド
    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SEVolume", seVolume);
        PlayerPrefs.Save(); // 確実に保存
    }
}

