using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectDetector : MonoBehaviour
{
    [Header("Elements")]
    public GameDirector gameDirector;
    public Door touchedDoor;


    [Header("Properties")]
    public Vector3 rayStartOffset;
    public float touchRange;
    public bool isTouchingDoor;

    

    private void Update()
    {
        Debug.DrawRay(transform.position + rayStartOffset, transform.forward * touchRange, Color.green);

        RaycastHit hit;

        //NOTE:
        //attigimiz raycast objeye vuruyor/carpiyor mu
        //method boolean doner, out ile method sonucunda elde edecegimiz sonuc hit'e kaydedilir
        //ilk parametre cizecegi yeri, ikinci parametre ise yonu belirtir
        if (Physics.Raycast(transform.position + rayStartOffset, transform.forward, out hit, touchRange))
        {
            Door door = hit.transform.GetComponentInParent<Door>();

            if (hit.transform.CompareTag("Door") &&
                !(door.isDoorOpened))
            {
                isTouchingDoor = true;
                //NOTE:
                //kapi kanatlari ebeveyn uzerinde oldugu icin parentini aldik
                touchedDoor = door;
                gameDirector.messageUI.ShowOpenDoorMessage();
            }
            else
            {
                isTouchingDoor = false;
                touchedDoor = null;
                gameDirector.messageUI.HideOpenDoorMessage();
                gameDirector.messageUI.HideDoorIsLockedMessage();
            }
        }
        else
        {
            isTouchingDoor = false;
            touchedDoor = null;
            gameDirector.messageUI.HideOpenDoorMessage();
            gameDirector.messageUI.HideDoorIsLockedMessage();

        }
    }

    public void OpenDoor()
    {
        touchedDoor.Open();

        if(touchedDoor.isDoorLocked)
        {
            UseKey();
        }
    }

    private void UseKey()
    {
        gameDirector.messageUI.HideKeyImage();
        gameDirector.playerHolder.isKeyCollected = false;
    }
}
