using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Elements")]
    public Image borderImage;
    public Image fillBar;
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPlayerHealthBar(float healthRatio)
    {
        fillBar.fillAmount = healthRatio;  
    }

    public void FlashBorder()
    {
        borderImage.DOColor(Color.red, .2f).SetLoops(2, LoopType.Yoyo);
    }
}
