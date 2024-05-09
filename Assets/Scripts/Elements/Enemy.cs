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
        //y�n vekt�r�
        var direction =  playerTransform.position - transform.position;

        //NOTE:
        //aradaki fark� hesaplay�p 0-1 aras�na �ekerek y�n vekt�r� elde eder
        //bunu yapmazsak vekt�r b�y�d�k�e ileride hesaplayaca��m�z h�z da b�y�r
        //sabit bir de�er i�in normalized etmek gereklidir
        var directionNormalized = direction.normalized;

        transform.position += directionNormalized * enemySpeed;
    }
}
