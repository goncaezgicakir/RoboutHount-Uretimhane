using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    private GameDirector _gameDirector;

    public void StartDiamond(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetCollected();
        }
    }

    private void GetCollected()
    {
        gameObject.SetActive(false);
        _gameDirector.DiamondCollected();
    }

}
