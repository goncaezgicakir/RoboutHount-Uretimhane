using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private Enemy _enemy;

    public EnemyBullet enemyBulletPrefab;
    public List<Transform> shootPositions;
    public float attackRate;

    private float _lastShootTime;
    public void StartEnemyWeapon(Enemy enemy)
    {
        _enemy = enemy;
    }


   
    public void TryShoot()
    {
        //NOTE:
        //bu sayede atislar aras�nda zaman gecmesi saglanir
        if (Time.time - _lastShootTime > attackRate)
        {
            foreach (Transform sp in shootPositions)
            {
                var newBullet = Instantiate(enemyBulletPrefab);

                //NOTE:
                //shoot positionlar enemy kollar�ndaki silahlarin ucuna denk gelicek
                //bulletlar bu noktalarda spawn olup
                //LookAt ile playera gore direcition�n� bulup ileri dogru hareket edebilir hale gelir
                newBullet.transform.position = sp.position;

                //NOTE:
                //burda transfrom componenti biraz yer seviyesine yak�n oldugu icin (ki genelde boyledir)
                //bir birim yukar� deger eklenir ki enemylerin atis yonu daha mantikli olabilsin
                newBullet.transform.LookAt(_enemy.playerTransform.position + Vector3.up * 1.5f);
            }

            _lastShootTime = Time.time;
        }
    }
}
