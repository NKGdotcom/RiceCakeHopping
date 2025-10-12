using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static ButtonAction;

public class ArrowButtonAction : MonoBehaviour,IPointerClickHandler
{
    public enum ArrowVaraiety { StageSelectRight,StageSelectLeft,HowToPlayRight,HowToPlayLeft }
    [Header("–îˆóƒ{ƒ^ƒ“‚ÌŽí—Þ")]
    [SerializeField] private ArrowVaraiety arrowVaraiety;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (arrowVaraiety)
        {
            case ArrowVaraiety.StageSelectRight:
                StageSelectPage.Instance.NextPage();
                break;
            case ArrowVaraiety.StageSelectLeft:
                StageSelectPage.Instance.BackPage();
                break;
            case ArrowVaraiety.HowToPlayRight:
                HowToPlayPage.Instance.NextPage();
                break;
            case ArrowVaraiety.HowToPlayLeft:
                HowToPlayPage.Instance.BackPage();
                break;
        }
    }
}
