using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //NOTE:
        //Inspectorda tan�mlanan Tag i�in comparision ko�ulu
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //NOTE:
            //bu �ekilde ilgili obje sahneden kald�r�l�cak
            collision.gameObject.SetActive(false);
        }
        
    }
}
