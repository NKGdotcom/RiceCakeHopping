using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�C�g����ʂ���i�߂邾���̃A�j���[�V����
/// </summary>
public class TitleOpen : MonoBehaviour
{
    private bool isTitleOpen;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown&&!isTitleOpen)
        {
            isTitleOpen = true;
            TitleAnimationState.Instance.MoveToTitleNext();
        }
    }
}
