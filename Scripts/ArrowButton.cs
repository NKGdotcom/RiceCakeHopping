using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowButton : OnMouse
{
    [SerializeField] private PageManger pageManager;
    [SerializeField] private int moveDirection = 1; //設定：進むボタンが1か-1か

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        if(pageManager != null)
        {
            pageManager.ChangePage(moveDirection);
        }
    }
}
