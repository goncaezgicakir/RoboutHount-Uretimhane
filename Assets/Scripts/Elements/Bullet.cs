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
        //Inspectorda tanimlanan Tag icin comparision kosulu
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //NOTE:
            //bu sekilde ilgili obje sahneden kaldirilacak
            collision.gameObject.SetActive(false);
        }
        
    }
}
