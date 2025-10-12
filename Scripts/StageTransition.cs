using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransition : MonoBehaviour
{
    public static StageTransition Instance { get; private set; }

    private Animator stageTransitionAnimator;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void Start()
    {
        stageTransitionAnimator = GetComponent<Animator>();
    }
}
