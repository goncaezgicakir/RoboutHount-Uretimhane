using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public TextMeshProUGUI openDoorMessage;
    public TextMeshProUGUI doorIsLockedMessage;
    //public Image backpackImage;
    public Image keyImage;

    public float keyFloat;

    private void Start()
    {
        HideDoorIsLockedMessage();
        HideOpenDoorMessage();
        HideKeyImage();

    }

    public void ShowOpenDoorMessage()
    {
        openDoorMessage.gameObject.SetActive(true);
    }

    public void HideOpenDoorMessage()
    {
        openDoorMessage.gameObject.SetActive(false);
    }

    public void ShowDoorIsLockedMessage()
    {
        doorIsLockedMessage.gameObject.SetActive(true);
    }

    public void HideDoorIsLockedMessage()
    {
        doorIsLockedMessage.gameObject.SetActive(false);
    }

    public void ShowKeyImage()
    {
        keyImage.gameObject.SetActive(true);
        keyImage.transform.localScale = Vector3.zero;
        //NOTE:
        //ease ile daha dinamik bir efect verdik
        keyImage.transform.DOScale(1, .3f).SetEase(Ease.OutBack);

        /*
        //NOTE:
        
        keyImage.gameObject.SetActive(true);
        keyImage.transform.localScale = Vector3.zero;
        sahnede gitmesini istediðimiz yere bir ýmage koyariz
        //once key scale ve move eder
        keyImage.transform.DOScale(2, .5f).SetLoops(2, LoopType.Yoyo);
        keyImage.transform.DOMoveX(backPackImage.transform.position.x, 1f);
        keyImage.transform.DOMoveY(keyImage.transform.position.y + 50, .5f).SetLoops(2, LoopType.Yoyo);
        //sonra backpack scale eder
        //git gel yapýcaðý icin loop koyduk
        //key ve backpack arasýnda bir delay olamsý gýruntu acýsýndan daha iyidir
        backPackImage.transform.DOScale(1.5f, .2f).SetLoops(2, LoopType.Yoyo).setDelay;;
        */

    }

    public void HideKeyImage()
    {
        keyImage.gameObject.SetActive(false);
    }
}
