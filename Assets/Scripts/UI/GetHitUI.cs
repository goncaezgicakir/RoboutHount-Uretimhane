using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitUI : MonoBehaviour
{

    public void ShowDamageEffect()
    {
        //NOTE:
        //?
        //fade olurken bir daha vurulursak su anda varolan anþmasyonu once oldur(DOKill),
        //sonra da alpha degerini(saydamlastirma) sifirla

        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOKill();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(.1f, .2f).SetLoops(2, LoopType.Yoyo);
    }

}
