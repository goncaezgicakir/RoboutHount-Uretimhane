using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Transform playerTransform;

    public float enemySpeed;

    //NOTE:
    //oyun basladiktan sonra transform degeri enemy spawn edlidiginde 
    //anlik olusur. bu nedenle assign etmek gerekir
    public void StartEnemy(Transform pTransform)
    {
        playerTransform = pTransform;
    }

    private void Update()
    {
        //oyuncu ve enemy arasýndaki mesafe vektoru
        var direction =  playerTransform.position - transform.position;

        //NOTE:
        //aradaki farki hesaplayip 0-1 arasina cekerek yon vektoru elde eder
        //bunu yapmazsak vektor buyudukce ileride hesaplayacagimiz hiz da buyur
        //sabit bir deger icin normalized etmek gereklidir
        var directionNormalized = direction.normalized;

        transform.position += directionNormalized * enemySpeed;
    }
}
