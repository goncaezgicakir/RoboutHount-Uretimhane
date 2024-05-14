using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public GameDirector gameDirector;

    public float playerSpeed;
    public float jumpForce;
    public Rigidbody playerRb;

    //NOTE:
    //Mesh bu gameObject icin hacim alanini belirtir
    public Transform playerMesh; 


    public void MovePlayer(Vector3 direction)
    {
        transform.position = transform.position + direction * playerSpeed;
    }

    public void MakePlayerJump()
    {
        playerRb.AddForce(new Vector3(0, 1, 0) * jumpForce);
    }

    public void RotatePlayer(Vector2 turn)
    {
        //NOTE:
        //Quaternion � Rotasyon yaparken belli bir noktaya gore rotasyon yapmamiz gerektigi i�in bir nokta
        //3 boyutla(x,y,z) ile tan�mlanirken 4. bir nokta da donus ekseni olarak tanimlanir.
        //Bu yapi icin kullanilan bir siniftir.
        //Euler acisi (x,y,z) axislerine g�re rotasyon icin kullanilan acidir
        var mouseSensitivity = gameDirector.inputManager.mouseSensitivity;
        playerMesh.localRotation = Quaternion.Euler(-turn.y * mouseSensitivity, turn.x * mouseSensitivity, 0);
    }
}
