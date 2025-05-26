using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundVolume", menuName = "ScriptableObjects/Sound/SoundVolume")]
public class SoundVolume : ScriptableObject//‚Ç‚±‚©‚ç‚Å‚à‰¹‚Ì‚‚³‚ð•Ï‚¦‚ç‚ê‚é‚æ‚¤‚É
{
    public float BGMVolume { get => bgmVolume; set => bgmVolume = value; }
    public float SEVolume { get => seVolume; set => seVolume = value; }

    [SerializeField][Range(0,1)] private float bgmVolume;
    [SerializeField][Range(0, 1)] private float seVolume;
}
