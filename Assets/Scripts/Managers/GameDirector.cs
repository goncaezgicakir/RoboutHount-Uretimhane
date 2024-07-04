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
    public LevelManager levelManager;
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
    public AdminUI adminUI;

    [Header("Elements")]
    public Transform enemy;
    public Player playerHolder;
    public Transform cameraTransform;

    [Header("Properties")]
    //NOTE:
    //default olarak boolean degeri false dur.
    public bool isGameStarted;
    public bool ingameControlsLocked;
    public int currentlyPlayedLevel;


    private void Start()
    {
        ingameControlsLocked = true;
        mainUI.Show();
        winUI.Hide();
        failUI.Hide();
    }

    // method called once per frame
    private void Update()
    {
       
    }

    public void StartGame(int levelId)
    {
        currentlyPlayedLevel = levelId;
        levelManager.ClearCurrentLevel();
        levelManager.CreateLevel(levelId);
        diamondManager.StartDiamondManager();
        Cursor.lockState = CursorLockMode.Locked;
        isGameStarted = true;
        enemyManager.SpawnWave();
        ingameControlsLocked = false;
        playerHolder.StartPlayer();
        healthBarUI.Show();
        playerHolder.ResetRigidbodyConstraints();
    }

    

    public void LevelCompleted()
    {
        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        winUI.Show();
        healthBarUI.Hide();

        //currently played leveldan bitirildiginde ulasilimis en buyuk leveldan buyuk ise 
        //ulasilan en son level degerine set edilir
        //bu kontrolun sebebi oyuncu eski levellara donup oynadiginda degerin degismemesi icin yapilir
        if (currentlyPlayedLevel > PlayerPrefs.GetInt("LastFinishedLevel"))
        {
            PlayerPrefs.SetInt("LastFinishedLevel", currentlyPlayedLevel);
            
        }

    }

    public void LevelFailed()
    {
        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        failUI.Show();
        healthBarUI.Hide();
        //yeni level yuklenirken player gravityden kaynakli dusmesin
        playerHolder.FreezePlayerYAxis();
        
    }
    
    public void DiamondCollected()
    {   
         LevelCompleted();
    }

    
}
