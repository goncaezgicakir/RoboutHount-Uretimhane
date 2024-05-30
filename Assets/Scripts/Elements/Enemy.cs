using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Transform _playerTransform;
    private Rigidbody _enemyRb;
   
    public EnemyHealthBar enemyHealthBar;
    
    public float enemySpeed;
    public float wheelRotationSpeed;
    public bool isEnemyMoving;
    public int startHealth;
    private int _currentHealth;

    public Transform leftWheel;
    public Transform rightWheel;

    //NOTE:
    //oyun basladiktan sonra transform degeri enemy spawn edlidiginde 
    //anlik olusur. bu nedenle assign etmek gerekir
    public void StartEnemy(Transform pTransform, EnemyManager enemyManager)
    {
        _playerTransform = pTransform;
        _enemyManager = enemyManager;
        _currentHealth = startHealth;
        _enemyRb = GetComponent<Rigidbody>();

        enemyHealthBar.StartEnemyHealthBar(enemyManager.gameDirector);
        //enemy full health iken barin gozukemesine gerek yok
        enemyHealthBar.Hide();
    }

    public void StartMoving()
    {
        isEnemyMoving = true;
    }

    private void Update()
    {   
        if (isEnemyMoving)
        {
            //oyuncu ve enemy arasýndaki mesafe vektoru
            var direction = _playerTransform.position - transform.position;

            //NOTE:
            //aradaki farki hesaplayip 0-1 arasina cekerek yon vektoru elde eder
            //bunu yapmazsak vektor buyudukce ileride hesaplayacagimiz hiz da buyur
            //sabit bir deger icin normalized etmek gereklidir
            var directionNormalized = direction.normalized;

            //NOTE:
            //time.deltaTime iki update arasinda gecen sureye esittir
            //fps farketmeden her bilgisayarda ayni calismasi saglanir
            transform.position += directionNormalized * enemySpeed * Time.deltaTime;
            
            //NOTE:
            //enemynin verilen NOKTAYA(vektor degil) bakmasi saglanir
            transform.LookAt(_playerTransform.position);

            //NOTE:
            //enemy robotunun y ekseninde tekerlek donusu
            rightWheel.Rotate(0, wheelRotationSpeed, 0);
            leftWheel.Rotate(0, wheelRotationSpeed, 0);

        }
    }


    public void EnemyGodHit(int damage, Vector3 pushDir, float pushPower)
    {
        ReduceHealth(damage);

        //bullet hit sound
        _enemyManager.gameDirector.audioManager.PlayMetalImpactSFX();
        PushEnemyBack(pushDir, pushPower);
        UpdateHealthBar();
    }

    private void PushEnemyBack(Vector3 pushDir, float pushPower)
    {
        _enemyRb.AddForce(pushDir * pushPower);

    }

    private void ReduceHealth(int damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            KillEnemy();
        }

        print(GetHealthRatio());
    }

    private void KillEnemy()
    {
        //enemymanagera haber verilir ki sahneden yeterli enemy var mi diye
        _enemyManager.EnemyDied(this);

        gameObject.SetActive(false);
        enemyHealthBar.Hide();
    }

    private void UpdateHealthBar()
    {
        if (GetHealthRatio() > 0)
        {

            enemyHealthBar.Show();
            enemyHealthBar.SetHealthRatio(GetHealthRatio());
        }
    }

    public float GetHealthRatio()
    {
        return (float)_currentHealth / startHealth;
    }
}
