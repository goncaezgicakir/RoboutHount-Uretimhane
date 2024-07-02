using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminUI : MonoBehaviour
{
    [Header("Elements")]
    public GameDirector gameDirector;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //NOTE:
    //bu method button uzerinde onClicked seceneginde eklendi
    public void ResetProgressButtonPressed()
    {
        Hide();
        //player leveli 0a set edilerek oyunun resetlenir
        PlayerPrefs.SetInt("PlayerLevel", 0);
        //ilk level yuklenir
        SceneManager.LoadScene (0);
    }
}
