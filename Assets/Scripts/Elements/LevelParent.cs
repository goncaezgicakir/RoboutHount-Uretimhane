using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParent : MonoBehaviour
{
    private GameDirector _gameDirector;

    public GateTrigger gateTrigger;
    public Transform enemyPlaceHoldersParent;
    public Transform diamondplaceHoldersParent;

    public void StartLevel(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
        gateTrigger.enemyManager = _gameDirector.enemyManager;
        _gameDirector.diamondManager.diamondPlaceHolderParent = diamondplaceHoldersParent;
        _gameDirector.enemyManager.enemyPlaceHolderParent = enemyPlaceHoldersParent;
    }
}
