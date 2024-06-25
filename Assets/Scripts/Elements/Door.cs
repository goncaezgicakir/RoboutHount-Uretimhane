using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public bool isDoorOpen;
    public void Open()
    {
        //NOTE:
        //ilk parametre yeni z konum degeri, ikinci parametre ise ne kadar surede gidecegini belirtir
        leftDoor.DOLocalMoveZ(1, .3f);
        rightDoor.DOLocalMoveZ(-2.5f, .3f);
        isDoorOpen = true;
    }
}
