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
        //Inspectorda tanýmlanan Tag için comparision koþulu
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //NOTE:
            //bu þekilde ilgili obje sahneden kaldýrýlýcak
            collision.gameObject.SetActive(false);
        }
        
    }
}
