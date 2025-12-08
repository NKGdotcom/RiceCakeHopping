using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    public enum RicecakeType
    {
        Kinako, Normal, Soysource,
    }
    public RicecakeType TargetRicecakeType { get => targetRicecakeType; private set => targetRicecakeType = value; }
    public float TargetSize { get => targetSize; private set => targetSize = value; }
    public float TimeLimit { get => timeLimit;}
    public string StageDescription { get => stageDescription; }
    
    [SerializeField] private RicecakeType targetRicecakeType;
    [SerializeField] private float targetSize;
    [SerializeField] private float timeLimit = 60f;
    [SerializeField] private string stageDescription;
}
