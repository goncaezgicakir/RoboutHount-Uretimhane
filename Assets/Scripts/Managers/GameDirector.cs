using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public InputManager inputManager;
    public EnemyManager enemyManager;
    public DiamondManager diamondManager;
    public MainUI mainUI;
    public WinUI winUI;

    public Transform enemy;
    public Player playerHolder;
    public Transform bulletSpawnPoint;
    public Bullet bulletPrefab;
    
    public Vector2 turn;
    public int bulletCount;
    public float maxSpread;
    //NOTE:
    //default olarak boolean degeri false dur.
    public bool isGameStarted;
    public bool ingameControlsLocked;

    private void Start()
    {
        ingameControlsLocked = true;
        mainUI.Show();
        winUI.Hide();
    }

    // method called once per frame
    private void Update()
    {
        if (isGameStarted && !ingameControlsLocked)
        {
            turn.x += Input.GetAxis("Mouse X");
            turn.y += Input.GetAxis("Mouse Y");
            playerHolder.RotatePlayer(turn);
        }
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGameStarted = true;
        enemyManager.SpawnWave();
        ingameControlsLocked = false;
    }

    public void SpawnBullets()
    {
        for(int i = 0; i < bulletCount; i++)
        {
            SpawnBullet();
        }
    }
    public void SpawnBullet()
    {
        //create randomly geenrated Vector3 for a spread of the bullet 
        var spread = new Vector3(Random.Range(-maxSpread, maxSpread),
                                 0,
                                 Random.Range(-maxSpread, maxSpread));
        //NOTE:
        //Instantiate prefab kullanilarak oyun sahnesine yeni bir nesne eklemek
        //icin kullanilan methoddur. Gerekliyse transform degeri de parametre olarak eklenebilir.
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnPoint.position;
        //NOTE:
        //forward VEKTORU spawn pointe EKLENEREK bulletin dogru yerden olusmasi saglanir (vektor toplamayi dusun)
        //eklemezsek surekli sifira yakin bir VEKTOR degeri position olarak algilanir ve 0,0,0 yakin bir yerden olusur
        //spread ekleyerek de olusan mermilerin olabilidigince farklý yonlere doðru olusmasi ve shotgun goruntusu 
        //vermesini istedik
        newBullet.transform.LookAt(bulletSpawnPoint.position + bulletSpawnPoint.forward + spread);
    }


    public void LevelCompleted()
    {
        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        winUI.Show();
    }    

    public void DiamondCollected()
    {   
        
         LevelCompleted();
    }
}
