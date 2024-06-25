using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectDetecter : MonoBehaviour
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
            if (hit.transform.CompareTag("Door"))
            {
                isTouchingDoor = true;
                //NOTE:
                //kapi kanatlari ebeveyn uzerinde oldugu icin parentini aldik
                touchedDoor = hit.transform.GetComponentInParent<Door>();
                gameDirector.messageUI.ShowOpenDoorMessage();
            }
            else
            {
                isTouchingDoor = false;
                touchedDoor = null;
                gameDirector.messageUI.HideOpenDoorMessage();
            }
        }
        else
        {
            isTouchingDoor = false;
            touchedDoor = null;
            gameDirector.messageUI.HideOpenDoorMessage();
        }
    }

    public void OpenDoor()
    {
        touchedDoor.Open();
    }
}
