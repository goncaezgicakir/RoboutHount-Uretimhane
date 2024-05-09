using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform playerTransform;

    public float enemySpeed;

    
    // Update is called once per frame
    void Update()
    {
        //yön vektörü
        var direction =  playerTransform.position - transform.position;

        //NOTE:
        //aradaki farký hesaplayýp 0-1 arasýna çekerek yön vektörü elde eder
        //bunu yapmazsak vektör büyüdükçe ileride hesaplayacaðýmýz hýz da büyür
        //sabit bir deðer için normalized etmek gereklidir
        var directionNormalized = direction.normalized;

        transform.position += directionNormalized * enemySpeed;
    }
}
