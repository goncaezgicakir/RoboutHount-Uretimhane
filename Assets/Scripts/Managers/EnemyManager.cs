using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public Enemy enemyPrefab;
    public int enemyCount;
    public List<Enemy> activeEnemies = new List<Enemy>();

    private int waveCount;

    private void Start()
    {
        waveCount = 1;
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
        //her yeni wavede daha fazla enemy oluþucak 10,20,30..
        for(int i = 0; i < enemyCount*waveCount; i++)
        {
            SpawnEnemy(i);
        }
        waveCount++;
    }

    private void SpawnEnemy(int x)
    {
        var newEnemy = Instantiate(enemyPrefab);
        //get a random circle offset as a vector3
        var circleOffset = Random.onUnitSphere;
        //create a new vector only with its x and z values
        //because we dont want to use y axis
        Vector3 offset = new Vector3(circleOffset.x, 0 , circleOffset.y);
        //add this offset to player tarnsform in order to create a new enemy tarnsform position
        //NOTE:
        //20 degeri zorluk için
        newEnemy.transform.position = gameDirector.playerHolder.transform.position +offset * 20;
        //assign the transform value
        newEnemy.StartEnemy(gameDirector.playerHolder.transform, this);
        //yeni olusan enemy listede tut
        activeEnemies.Add(newEnemy);
    }

    public void EnemyDied(Enemy enemy)
    {
        //ilgili enemy objesini enemyManagerdaki aktif enemy listesinden cikar
        activeEnemies.Remove(enemy);

        //sahnede enemy kalmazsa yenilerini olusturuz
        if (activeEnemies.Count <= 0)
        {   
            if(waveCount <= 3)
            {
                SpawnWaveDelayed(2);
            }
            else
            {
                gameDirector.LevelCompleted();
            }
        }
    }
}
