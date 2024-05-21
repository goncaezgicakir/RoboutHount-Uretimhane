using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Transform _playerTransform;
    public float enemySpeed;
    public bool isEnemyMoving;

    //NOTE:
    //oyun basladiktan sonra transform degeri enemy spawn edlidiginde 
    //anlik olusur. bu nedenle assign etmek gerekir
    public void StartEnemy(Transform pTransform, EnemyManager enemyManager)
    {
        _playerTransform = pTransform;
        _enemyManager = enemyManager;
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
        }
    }

    public void EnemyGodHit()
    {
        gameObject.SetActive(false);

        //enemymanagera haber verilir ki sahneden yeterli enemy var mi diye
        _enemyManager.EnemyDied(this);
    }
}
