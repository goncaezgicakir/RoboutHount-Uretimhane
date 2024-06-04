using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public Transform placeHolderParent;
    public Enemy enemyPrefab;
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
        foreach (Transform ph in placeHolderParent)
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
    }
}
