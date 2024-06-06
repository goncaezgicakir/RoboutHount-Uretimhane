using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [Header("Elements")]
    public EnemyManager enemyManager;


    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyManager.AlertEnemies();
            //oyuncu bir kere gate triggerdan gectikten sonra
            //box collider componenti kaldirilir
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
