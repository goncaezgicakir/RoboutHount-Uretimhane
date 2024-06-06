using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    //NOTE:
    //Header ile Unity editor uzerinde degiskenleri bu isimli grup altinda toparlar
    [Header("Elements")]
    private MeshRenderer _bulletMesh;
    private SphereCollider _bulletCollider;

    [Header("Properties")]
    public float bulletSpeed;
    public float bulletLifeTime;
    public float pushPower;
    public int damage;
    private float _bulletStartTime;


    private void Start()
    {
        _bulletStartTime = Time.time;
        _bulletMesh = GetComponentInChildren<MeshRenderer>();
        _bulletCollider =  GetComponent<SphereCollider>();
    }

    private void Update()
    {
        //bullet start time bulletLifeTime astiysa yok olmali
        if(Time.time - _bulletStartTime > bulletLifeTime)
        {
            DestroyBullet();
        }
        else
        {
            Move();
        }
    }

    private void DestroyBullet()
    {
        //NOTE:
        //bulleta ait mesh ve colliderlar kapatýlýr
        _bulletMesh.enabled = false;
        _bulletCollider.enabled = false;
        //NOTE:
        //destroydan once biraz bekledik cunku bulletin taili(effect) da sureyle uyumlu olarak yok olmali
        Destroy(gameObject, 2f);
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
            //su anda sadece collider elimizde bize objenþn kendisi lazým
            //ona bu sekilde ulasabiliriz
            //NOTE:
            //EnemyGotHit ikinci parametresi bulletin forward yonune gore belirlenmeli
            other.GetComponent<Enemy>().EnemyGodHit(damage, transform.forward, pushPower);

            //carpan mermiyi (kendisini) de sahneden kaldiralim
            DestroyBullet();
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            //carpan mermiyi (kendisini) ve carpistigi objeyi kaldiralim
            Destroy(other.gameObject);
            DestroyBullet();
        }
        else if (other.CompareTag("MapObjects"))
        {
            //alakasiz bir yere carpan mermiyi (kendisini) de sahneden kaldiralim
            DestroyBullet();
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
