using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル画面から進めるだけのアニメーション
/// </summary>
public class TitleOpen : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("タイトル画面でのアニメーション")]
    [SerializeField] private TitleAnimation titleAnimation; 
    //一回のみアニメーションでタイトル画面を開く
    private bool isTitleOpen;

    private void Awake()
    {
        isTitleOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
        //一度のみタイトルからステージ選択、遊び方画面に向かう
        if (Input.anyKeyDown&&!isTitleOpen)
        {
            //タイトル画面を開く
            isTitleOpen = true;
            titleAnimation.MovetoTitleNext();
        }
    }
}
