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

    private void Move()
    {
        transform.position += transform.forward * bulletSpeed;
    }

    //NOTE:
    //carpisma(collision) soncuu carpisilan objenin collideri parametrede gelir
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //NOTE:
            //su anda sadece collider elimizde bize objenþn kendisi lazým
            //ona bu sekilde ulasabiliriz
            other.GetComponent<Enemy>().EnemyGodHit();
            //carpan mermiyi (kendisini) de sahneden kaldiralim
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("MapObjects"))
        {
            //alakasiz bir yere carpan mermiyi (kendisini) de sahneden kaldiralim
            gameObject.SetActive(false);
        }
    }


    /*

    //NOTE:
    //Collision carpismayi Collider objelerin sekline gore carpisabilecegi alaný tutar
    private void OnCollisionEnter(Collision collision)
    {
        //NOTE:
        //Inspectorda tanimlanan Tag icin comparision kosulu
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //NOTE:
            //bu sekilde ilgili obje(enemy) sahneden kaldirilacak
            collision.gameObject.SetActive(false);
            //carpan mermiyi (kendisini) de sahneden kaldiralim
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("MapObjects")){

            //alakasiz bir yere carpan mermiyi (kendisini) de sahneden kaldiralim
            gameObject.SetActive(false);
        }
        
    }

    */
}
