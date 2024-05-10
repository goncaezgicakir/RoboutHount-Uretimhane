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
        //oyun basladiginda cursor gozukmesin
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        //NOTE:
        //•Transform üzerinden forward, -forward, right, -right ile
        //yon degiskeni Vector3 alabiliriz.
        //Transform degerinin y ekseni 0 tutulursa
        //nesne tek düzlemde hareket ettirilebilir.

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
        //GetMouseButtonDown(0) – sol click icin, down tek sefer basmak icin 
        //GetMouseButtonDown(1) – sag click icin, down tek sefer basmak icin
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
        //Quaternion – Rotasyon yaparken belli bir noktaya gore rotasyon yapmamiz gerektigi için bir nokta
        //3 boyutla(x,y,z) ile tanýmlanirken 4. bir nokta da donus ekseni olarak tanimlanir.
        //Bu yapi icin kullanilan bir siniftir.
        //Euler acisi (x,y,z) axislerine göre rotasyon icin kullanilan acidir
        player.localRotation = Quaternion.Euler(-turn.y * mouseSensitivity, turn.x * mouseSensitivity, 0);
    }
}
