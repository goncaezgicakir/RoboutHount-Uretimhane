using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Manaagers")]
    public Settings settings;

    [Header("Elements")]
    public GameDirector gameDirector;
    public Weapon weapon;
    public ObjectDetecter objectDetecter;

    //NOTE:
    //Mesh bu gameObject icin sahnedeki hacim alanini belirtir
    public Transform playerMesh;
    public Rigidbody playerRb;

    [Header("Properties")]
    public float recoilForce;
    public float speedMultiplier;
    public int startHealth;
    private int _currentHealth;
    public bool isTouchingGround;

    public void StartPlayer()
    {
        _currentHealth = startHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Heal"))
        {
            GetHealed();
            collision.gameObject.SetActive(false);
            gameDirector.fXManager.PlayHealCollectedFX(collision.transform.position);
            gameDirector.audioManager.PlayHealSFX();

        }
    }

    private void GetHealed()
    {
        //healthi arttir
        _currentHealth += Mathf.RoundToInt(startHealth * .5f);

        //current health start healthi asmamali
        if(_currentHealth > startHealth){
            _currentHealth = startHealth;
        }

        //health bar guncelle
        gameDirector.healthBarUI.SetPlayerHealthBar(GetHealthRatio());
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MapObjects"))
        {
            isTouchingGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MapObjects"))
        {
            isTouchingGround = false;
        }
    }

   


    public void PlayerGotHit(int damage)
    {
        ReduceHealth(damage);
        gameDirector.healthBarUI.SetPlayerHealthBar(GetHealthRatio());
        gameDirector.getHitUI.ShowDamageEffect();
    }

    public float GetHealthRatio()
    {
        return (float)_currentHealth / startHealth;
    }

    private void ReduceHealth(int damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0 )
        {
            gameDirector.LevelFailed();
        }

    }

    public void MovePlayer(Vector3 direction)
    {
        //NOTE:
        //time.deltaTime iki update arasinda gecen sureye esittir
        //fps farketmeden her bilgisayarda ayni calismasi saglanir
        transform.position += direction * settings.playerSpeed * Time.deltaTime * speedMultiplier;
    }

    public void MakePlayerJump()
    {
        playerRb.AddForce(new Vector3(0, 1, 0) * settings.jumpForce);
    }

    public void RotatePlayer(Vector2 turn)
    {
        //NOTE:
        //Quaternion – Rotasyon yaparken belli bir noktaya gore rotasyon yapmamiz gerektigi için bir nokta
        //3 boyutla(x,y,z) ile tanýmlanirken 4. bir nokta da donus ekseni olarak tanimlanir.
        //Bu yapi icin kullanilan bir siniftir.
        //Euler acisi (x,y,z) axislerine göre rotasyon icin kullanilan acidir
        var mouseSensitivity = gameDirector.inputManager.mouseSensitivity;
        playerMesh.localRotation = Quaternion.Euler(-turn.y * mouseSensitivity, turn.x * mouseSensitivity, 0);
    }

    public void PushPlayerBack()
    {
        //NOTE:
        //playerin shotgundan dolayi geri tepmesini temsil etmek icin backward force ekledik
        playerRb.AddForce(-playerMesh.transform.forward * recoilForce);
    }
}
