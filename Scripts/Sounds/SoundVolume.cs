using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObjectとして保存し、シーン間でデータを共有
/// </summary>
[CreateAssetMenu(fileName = "SoundVolune", menuName = "Scriptable Objects/SoundVolume")]
public class SoundVolume : ScriptableObject
{
    public float BGMVolume { get => bgmVolume; set => bgmVolume = value; }
    public float SEVolume { get => seVolume; set => seVolume = value; }
    [Header("サウンドの音量")]
    [Tooltip("BGMのボリューム")]
    [SerializeField] [Range(0,1)] private float bgmVolume;
    [Tooltip("SEのボリューム")]
    [SerializeField] [Range(0,1)] private float seVolume;
}
