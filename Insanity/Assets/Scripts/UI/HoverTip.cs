using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipTitle;
    public string tipDesc;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowMessage();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverTipManager.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        HoverTipManager.OnMouseHover(tipTitle, tipDesc, Input.mousePosition);
    }
}
