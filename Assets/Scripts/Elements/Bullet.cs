using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;

    private float _bulletStartTime;


    private void Start()
    {
        _bulletStartTime = Time.time;
    }

    private void Update()
    {
        Move();

        //bullet start time bulletLifeTime astiysa yok olmali
        if(Time.time - _bulletStartTime > bulletLifeTime)
        {
           gameObject.SetActive(false);
        }
    }

    private void Move()
    {
        //NOTE:
        //time.deltaTime iki update arasinda gecen sureye esittir
        //fps farketmeden her bilgisayarda ayni calismasi saglanir
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    //NOTE:
    //carpisma(collision) soncuu carpisilan objenin collideri parametrede gelir
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //NOTE:
            //su anda sadece collider elimizde bize objen�n kendisi laz�m
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
    //Collision carpismayi Collider objelerin sekline gore carpisabilecegi alan� tutar
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
