using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
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
    public void StartGameButtonPressed()
    {
        Hide();
        gameDirector.StartGame();
    }
}
