using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル画面から進めるだけのアニメーション
/// </summary>
public class TitleOpen : MonoBehaviour
{
    [SerializeField] private TitleAnimation titleAnimation; 
    private bool isTitleOpen;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&&!isTitleOpen)
        {
            isTitleOpen = true;
            titleAnimation.MovetoTitleNext();
        }
    }
}
