using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipTitle;
    public TextMeshProUGUI tipDesc;
    public RectTransform tipWindow;

    public static Action<string, string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        tipTitle.text = default;
        tipDesc.text = default;
        tipWindow.gameObject.SetActive(false);
    }

    private void ShowTip(string TipTitle, string TipDesc, Vector2 mousePos)
    {
        tipTitle.text = TipTitle;
        tipDesc.text = TipDesc;

        tipWindow.sizeDelta = new Vector2(400f, tipDesc.preferredHeight + 50f);
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    private void HideTip()
    {
        tipTitle.text = default;
        tipDesc.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
