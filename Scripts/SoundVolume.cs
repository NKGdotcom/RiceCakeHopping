using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundVolume", menuName = "ScriptableObjects/Sound/SoundVolume")]
public class SoundVolume : ScriptableObject//�ǂ�����ł����̍�����ς�����悤��
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

    // PlayerPrefs�Ɍ��݂̒l��ۑ����邽�߂̃��\�b�h
    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SEVolume", seVolume);
        PlayerPrefs.Save(); // �m���ɕۑ�
    }
}

