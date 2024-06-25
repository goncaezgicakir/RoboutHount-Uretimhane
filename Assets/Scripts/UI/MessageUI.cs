using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public TextMeshProUGUI openDoorMessage;
    
    public void ShowOpenDoorMessage()
    {
        openDoorMessage.gameObject.SetActive(true);
    }

    public void HideOpenDoorMessage()
    {
        openDoorMessage.gameObject.SetActive(false);
    }
}
