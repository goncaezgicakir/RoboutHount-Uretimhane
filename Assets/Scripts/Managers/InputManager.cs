using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [Header("Elements")]
    public GameDirector gameDirector;
    public Transform playerMesh;


    [Header("Properties")]
    public float mouseSensitivity;
    public Vector2 turn;


    private void Update()
    {
        if (!gameDirector.ingameControlsLocked)
        {
            if (gameDirector.isGameStarted)
            {
                turn.x += Input.GetAxis("Mouse X");
                //turn.y += Input.GetAxis("Mouse Y");
                //NOTE:
                //Clamp ile y eksenindeki donusu sinirlandirdik
                //(degerleri deneyerek print() ile debug ederek editorden bulduk)
                //turn.y = Mathf.Clamp(turn.y, -7f, 25f);

                gameDirector.playerHolder.RotatePlayer(turn);
            }

            //NOTE:
            //•Transform üzerinden forward, -forward, right, -right ile
            //yon degiskeni Vector3 alabiliriz.
            //Transform degerinin y ekseni 0 tutulursa
            //nesne tek düzlemde hareket ettirilebilir.

            //backward
            if (Input.GetKey(KeyCode.S))
            {
                var direction = -playerMesh.forward;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }

            //forward
            if (Input.GetKey(KeyCode.W))
            {
                var direction = playerMesh.forward;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }

            //left
            if (Input.GetKey(KeyCode.A))
            {
                var direction = -playerMesh.right;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }

            //right
            if (Input.GetKey(KeyCode.D))
            {
                var direction = playerMesh.right;
                direction.y = 0;
                gameDirector.playerHolder.MovePlayer(direction);
            }

            //jump
            if (Input.GetKeyDown(KeyCode.Space) && gameDirector.playerHolder.isTouchingGround)
            {
                gameDirector.playerHolder.MakePlayerJump();
            }

            //NOTE:
            //GetMouseButtonDown(0) – sol click icin, down tek sefer basmak icin 
            //GetMouseButtonDown(1) – sag click icin, down tek sefer basmak icin
            //load shotgun
            if (Input.GetMouseButtonDown(0))
            {
                gameDirector.playerHolder.weapon.StartLoadingShotgunCoroutine();
            }
            //stop loading shotgun
            if (Input.GetMouseButtonUp(0))
            {
                gameDirector.playerHolder.weapon.StopLoadShotgunCoroutine();
                gameDirector.playerHolder.weapon.TryShoot();
            }

            //close the game
            if (Input.GetKeyDown(KeyCode.F1))
            {
                //open the mainUI when the game is closed
                gameDirector.mainUI.Show();
                Cursor.lockState = CursorLockMode.None;

            }
            //close the game
            if (Input.GetKeyDown(KeyCode.F5))
            {
                //NOTE:
                //aktif sahne restart edilir
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //cursor aktif olsun
                Cursor.lockState = CursorLockMode.None;
            }

            //spawn an enemy
            if (Input.GetKeyDown(KeyCode.P))
            {
                gameDirector.enemyManager.SpawnWave();
            }

            //make player faster
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                gameDirector.playerHolder.speedMultiplier = 1.6f;
            }

            //make player slower
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                gameDirector.playerHolder.speedMultiplier = 1f;
            }
        }
    }
}
