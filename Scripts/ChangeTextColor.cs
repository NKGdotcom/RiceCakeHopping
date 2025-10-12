using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTextColor : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Animator changeTextColorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        changeTextColorAnimator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        changeTextColorAnimator.SetBool("ChangeRed", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        changeTextColorAnimator.SetBool("ChangeRed", false);
    }
}
