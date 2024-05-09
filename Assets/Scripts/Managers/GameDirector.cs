using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Transform enemy;
    public Transform player;
    public Transform playerHolder;
    public Transform bulletSpawnPoint;

    public Rigidbody playerRb;


    public float playerSpeed;
    public float jumpForce;
    public float mouseSensitivity;

    public Vector2 turn;
    public Bullet bulletPrefab;
    
    

    // Start is called before the first frame update
    void Start()
    {
        //oyun ba�lad���nda cursor g�z�kmesin
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {   
        //backward
        if (Input.GetKey(KeyCode.S))
        {
            var direction = -player.forward;
            direction.y = 0;
            MovePlayer(direction);
        }

        //forward
        if (Input.GetKey(KeyCode.W))
        {
            var direction = player.forward;
            direction.y = 0;
            MovePlayer(direction);
        }

        //left
        if (Input.GetKey(KeyCode.A))
        {
            var direction = -player.right;
            direction.y = 0;
            MovePlayer(direction);
        }

        //right
        if (Input.GetKey(KeyCode.D))
        {
            var direction = player.right;
            direction.y = 0;
            MovePlayer(direction);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakePlayerJump();
        }

        //NOTE:
        //GetMouseButtonDown(0) � sol click i�in, down tek sefer basmak i�in 
        //GetMouseButtonDown(1) � sa� click i�in, down tek sefer basmak i�in
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }

        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        RotatePlayer();
    }

    private void SpawnBullet()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnPoint.position;
        //NOTE:
        //forward VEKTORU spawn pointe EKLENEREK bullet�n do�ru yerden olu�mas� sa�lan�r (vektor toplamay� d���n)
        //eklemezsek s�rekli s�f�ra yak�n bir VEKTOR de�eri position olarak alg�lan�r ve 0,0,0 yak�n bir yerden olu�ur
        newBullet.transform.LookAt(bulletSpawnPoint.position + bulletSpawnPoint.forward);
    }

    //Methods

    void MovePlayer(Vector3 direction)
    {
        playerHolder.position = playerHolder.position + direction * playerSpeed;
    }

    void MakePlayerJump()
    {
        playerRb.AddForce(new Vector3(0,1,0) * jumpForce);
    }

    void RotatePlayer()
    {
        //NOTE:
        //Quaternion � Rotasyon yaparken belli bir noktaya g�re rotasyon yapmam�z gerekti�i i�in bir nokta
        //3 boyutla(x,y,z) ile tan�mlan�rken 4. bir nokta da d�n�� ekseni olarak tan�mlan�r.
        //Bu yap� i�in kullan�lan bir s�n�ft�r.
        player.localRotation = Quaternion.Euler(-turn.y * mouseSensitivity, turn.x * mouseSensitivity, 0);
    }
}
