using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadNextLevelButtonPressed()
    {
        Hide();
        //aktif sahne restart edilir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
