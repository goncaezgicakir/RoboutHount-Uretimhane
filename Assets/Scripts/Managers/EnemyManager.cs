using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Elements")] 
    public GameDirector gameDirector;
    public Transform enemyPlaceHolderParent;
    public Enemy enemyPrefab;
    public PowerUp healPowerUpPrefab;

    [Header("Properties")]
    public List<Enemy> activeEnemies = new List<Enemy>();

    public void AlertEnemies()
    {
        foreach (Enemy enemy in activeEnemies)
        {
            enemy.StartMoving();
        }
    }

    public void SpawnWaveDelayed(float delay)
    {   
        //NOTE:
        //gecikmeli olarak spawnWave methodunu baslatmak icin
        //ileride daha farkli kullanimi tercih edicez
        //Invoke("SpawnWave, 3)
        Invoke(nameof(SpawnWave), delay);
    }


    public void SpawnWave()
    {
        foreach (Transform ph in enemyPlaceHolderParent)
        {
            SpawnEnemy(ph.position);
            ph.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void SpawnEnemy(Vector3 position)
    {
        var newEnemy = Instantiate(enemyPrefab);        
        newEnemy.transform.position = position;

        //assign the transform value
        newEnemy.StartEnemy(gameDirector.playerHolder.transform, this);
        //yeni olusan enemy listede tut
        activeEnemies.Add(newEnemy);


        /*

        //get a random circle offset as a vector3
        var circleOffset = Random.onUnitSphere;
        //create a new vector only with its x and z values
        //because we dont want to use y axis
        Vector3 offset = new Vector3(circleOffset.x, 0, circleOffset.y);

        */
    }

    public void EnemyDied(Enemy enemy)
    {
        //ilgili enemy objesini enemyManagerdaki aktif enemy listesinden cikar
        activeEnemies.Remove(enemy);

       //tum enemyler olduyse artik diamond spawn edebiliriz
       if(activeEnemies.Count == 0)
       {
            gameDirector.diamondManager.SpawnDiamonds();
       }

       //NOTE:
       //settingsteki sans deegerine gore calýsýr
       if(Random.value < gameDirector.settings.healSpawnChange) {

            SpawnPowerUp(enemy);
       }
    }

    //power up olen enemynin pozisyonunda olusur
    private void SpawnPowerUp(Enemy enemy)
    {
        var newPowerUp = Instantiate(healPowerUpPrefab);
        newPowerUp.transform.position = enemy.transform.position + Vector3.up;
        //NOTE:
        //rastgele bir force ile gameobject dusme goruntusu vermek icin
        newPowerUp.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-200, 200), 200, Random.Range(-200, 200)));
       
        newPowerUp.startPowerUp(gameDirector.playerHolder);
    }
}
