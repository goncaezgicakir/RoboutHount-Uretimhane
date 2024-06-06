using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    private MeshRenderer _bulletMesh;
    private SphereCollider _bulletCollider;

    [Header("Properties")]
    public float bulletSpeed;
    public float bulletLifeTime;
    public int damage;

    private float _bulletStartTime;


    private void Start()
    {
        _bulletStartTime = Time.time;
        _bulletMesh = GetComponentInChildren<MeshRenderer>();
        _bulletCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        //bullet start time bulletLifeTime astiysa yok olmali
        if (Time.time - _bulletStartTime > bulletLifeTime)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerGotHit(damage);
        }
    }
}
