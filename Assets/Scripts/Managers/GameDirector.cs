using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [Header("Managers")]
    public InputManager inputManager;
    public EnemyManager enemyManager;
    public DiamondManager diamondManager;
    public AudioManager audioManager;
    public FXManager fXManager;
    public Settings settings;

    [Header("UI")]
    public MainUI mainUI;
    public WinUI winUI;
    public FailUI failUI;
    public HealthBarUI healthBarUI;
    public GetHitUI getHitUI;
    public MessageUI messageUI;

    [Header("Elements")]
    public Transform enemy;
    public Player playerHolder;
    public Transform cameraTransform;

    [Header("Properties")]
    //NOTE:
    //default olarak boolean degeri false dur.
    public bool isGameStarted;
    public bool ingameControlsLocked;
    public int desiredLevelIndex;


    private void Start()
    {
        print(SceneManager.GetActiveScene().buildIndex);
        //NOTE:
        //sahne(level) yukleme loopa girmesin diye kontrol eklenir
        var currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentLevelIndex != desiredLevelIndex)
        {
            //indexe gore olan sahne yuklenir
            //index degeri build settings altinda yer alir
            SceneManager.LoadScene(currentLevelIndex);
        }
        ingameControlsLocked = true;
        mainUI.Show();
        winUI.Hide();
        failUI.Hide();
    }

    // method called once per frame
    private void Update()
    {
       
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGameStarted = true;
        enemyManager.SpawnWave();
        ingameControlsLocked = false;
        playerHolder.StartPlayer();
        healthBarUI.Show();
    }

    

    public void LevelCompleted()
    {
        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        winUI.Show();
        healthBarUI.Hide();
        //next level indexi guncellendi
        desiredLevelIndex += 1;
    }

    public void LevelFailed()
    {
        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        failUI.Show();
        healthBarUI.Hide();
    }
    
    public void DiamondCollected()
    {   
        
         LevelCompleted();
    }

    
}
