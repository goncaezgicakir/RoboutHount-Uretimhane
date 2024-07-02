using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
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

    public void LoadNextLevelButton()
    {
        Hide();
        gameDirector.playerHolder.ResetPosition();
        gameDirector.mainUI.Show();
    }
}
