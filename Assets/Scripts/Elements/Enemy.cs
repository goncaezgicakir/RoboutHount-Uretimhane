using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Transform _playerTransform;
    public float enemySpeed;

    //NOTE:
    //oyun basladiktan sonra transform degeri enemy spawn edlidiginde 
    //anlik olusur. bu nedenle assign etmek gerekir
    public void StartEnemy(Transform pTransform, EnemyManager eManager)
    {
        _playerTransform = pTransform;
        _enemyManager = eManager;
    }

    private void Update()
    {
        //oyuncu ve enemy arasýndaki mesafe vektoru
        var direction =  _playerTransform.position - transform.position;

        //NOTE:
        //aradaki farki hesaplayip 0-1 arasina cekerek yon vektoru elde eder
        //bunu yapmazsak vektor buyudukce ileride hesaplayacagimiz hiz da buyur
        //sabit bir deger icin normalized etmek gereklidir
        var directionNormalized = direction.normalized;

        transform.position += directionNormalized * enemySpeed;
    }

    public void EnemyGodHit()
    {
        gameObject.SetActive(false);

        //enemymanagera haber verilir ki sahneden yeterli enemy var mi diye
        _enemyManager.EnemyDied(this);
    }
}
