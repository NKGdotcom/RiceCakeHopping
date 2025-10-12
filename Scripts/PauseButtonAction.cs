using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButtonAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator pauseAnimator;

    public static PauseButtonAction pauseButtonAction { get; private set; }
    private void Start()
    {
        pauseAnimator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        pauseAnimator.SetBool("ButtonEnter", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pauseAnimator.SetBool("ButtonEnter", false);

    }
}
