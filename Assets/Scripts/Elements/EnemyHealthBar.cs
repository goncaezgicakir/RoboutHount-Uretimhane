using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{   
    private GameDirector _gameDirector;

    public Transform fillParent;
    public GameObject healthBarBackground;

    //NOTE:
    //Awake, Start, Update sonrasý goruntu kameraya verilmeden hemen 
    //once calisir
    private void LateUpdate()
    {   
        //helath bar surekli olarak cameranin oldugu yone baksýn
        transform.LookAt(_gameDirector.cameraTransform.position);
    }

    public void StartEnemyHealthBar(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
    }
    public void Show()
    {
        healthBarBackground.SetActive(true);
    }
    public void Hide()
    {
        healthBarBackground.SetActive(false);
    }
    public void SetHealthRatio(float ratio)
    {   
        //NOTE:
        //Health bar sadece x ekseninde scale edilmeli
        fillParent.transform.localScale = new Vector3 (ratio, 1, 1);
    }

   

    
}
