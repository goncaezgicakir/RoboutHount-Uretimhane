using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public List<LevelParent> levels;
    public LevelParent currentLevel;
    
    public void ClearCurrentLevel()
    {
        if (currentLevel != null)
        {
            //remove the previous level map
            Destroy(currentLevel.gameObject);
        }
    }

    public void CreateLevel(int levelIndex)
    {
        currentLevel = Instantiate(levels[levelIndex-1]);
        currentLevel.StartLevel(gameDirector);
    }
}
