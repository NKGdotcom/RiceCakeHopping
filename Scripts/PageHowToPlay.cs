using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// �^�C�g���̗V�ѕ��y�[�W��UIPageMove��p���Čp������
/// </summary>
public class PageHowToPlay : UIPageMove
{
    private void Awake()
    {
        movePage = (eventData) => { TitleAnimationState.Instance.MoveToHowToPlayPage(); };
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
