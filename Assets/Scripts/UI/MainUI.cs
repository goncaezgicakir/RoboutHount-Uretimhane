using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Elements")]
    public GameDirector gameDirector;

    public List<Button> levelButtons;


    public void Show()
    {
        gameObject.SetActive(true);
        var lastFinishedLevel = PlayerPrefs.GetInt("LastFinishedLevel");
        Debug.Log(lastFinishedLevel);
        //lastFinishedLevel degerinden yuksek level id degerlerine ait
        //butonlarý interactable degeri false yapilir
        for(int i=0; i<levelButtons.Count; i++)
        {
            if(i <= lastFinishedLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //NOTE:
    //bu method button uzerinde onClicked seceneginde eklendi
    public void StartLevelButtonPressed(int levelId)
    {
        Hide();
        gameDirector.StartGame(levelId);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
