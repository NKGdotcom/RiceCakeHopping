using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundVolune", menuName = "Scriptable Objects/SoundVolume")]
public class SoundVolume : ScriptableObject
{
    public float BGMVolume { get => bgmVolume; set => bgmVolume = value; }
    public float SEVolume { get => seVolume; set => seVolume = value; }
    [SerializeField] [Range(0,1)] private float bgmVolume;
    [SerializeField] [Range(0,1)] private float seVolume;
}
