using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIPanelScale : MonoBehaviour
{
    public RectTransform UIPanel;
    private void Start()
    {
        UIFX();
    }
    public void UIFX()
    {
        UIPanel.localScale = Vector3.zero;
        UIPanel.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
    }
}
