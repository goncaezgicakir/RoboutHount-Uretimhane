using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public Enemy enemyPrefab;
    public int enemyCount;
    
    public void SpawnWave()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(i);
        }
        
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
        newEnemy.StartEnemy(gameDirector.playerHolder.transform);
    }
}
