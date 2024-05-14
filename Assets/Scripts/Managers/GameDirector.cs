using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public InputManager inputManager;
    public EnemyManager enemyManager;
    public MainUI mainUI;

    public Transform enemy;
    public Player playerHolder;
    public Transform bulletSpawnPoint;
    public Bullet bulletPrefab;
    public Vector2 turn;

    //NOTE:
    //default olarak boolean degeri false dur.
    public bool isGameStarted;



    // method called once per frame
    private void Update()
    {
        if (isGameStarted)
        {
            print("started");
            turn.x += Input.GetAxis("Mouse X");
            turn.y += Input.GetAxis("Mouse Y");
            playerHolder.RotatePlayer(turn);
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SpawnBullet()
    {
        //NOTE:
        //Instantiate prefab kullanilarak oyun sahnesine yeni bir nesne eklemek
        //icin kullanilan methoddur. Gerekliyse transform degeri de parametre olarak eklenebilir.
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnPoint.position;
        //NOTE:
        //forward VEKTORU spawn pointe EKLENEREK bulletin dogru yerden olusmasi saglanir (vektor toplamayi dusun)
        //eklemezsek surekli sifira yakin bir VEKTOR degeri position olarak algilanir ve 0,0,0 yakin bir yerden olusur
        newBullet.transform.LookAt(bulletSpawnPoint.position + bulletSpawnPoint.forward);
    }

    
}
