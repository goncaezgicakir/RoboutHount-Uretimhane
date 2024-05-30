using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class GameDirector : MonoBehaviour
{
    public InputManager inputManager;
    public EnemyManager enemyManager;
    public DiamondManager diamondManager;
    public AudioManager audioManager;
    public MainUI mainUI;
    public WinUI winUI;

    public Transform enemy;
    public Player playerHolder;
    public Transform bulletSpawnPoint;
    public Bullet bulletPrefab;
    public Transform cameraTransform;
    
    public Vector2 turn;
    public int bulletCount;
    public float maxSpread;
    public float shotgunLoadTime;
    //NOTE:
    //default olarak boolean degeri false dur.
    public bool isGameStarted;
    public bool ingameControlsLocked;
    public bool isShotgunLoaded;

    //NOTE:
    //Coroutine degiskeni ile coroutine methodu resultina yeniden erisebilriz
    private Coroutine _loadShotgunCoroutine;

    private void Start()
    {
        ingameControlsLocked = true;
        mainUI.Show();
        winUI.Hide();
    }

    // method called once per frame
    private void Update()
    {
        if (isGameStarted && !ingameControlsLocked)
        {
            turn.x += Input.GetAxis("Mouse X");
            turn.y += Input.GetAxis("Mouse Y");
            //NOTE:
            //Clamp ile y eksenindeki donusu sinirlandirdik
            //(degerleri deneyerek print() ile debug ederek editorden bulduk)
            turn.y = Mathf.Clamp(turn.y, -7f, 25f);

            playerHolder.RotatePlayer(turn);
        }
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGameStarted = true;
        enemyManager.SpawnWave();
        ingameControlsLocked = false;
    }

    public void StartLoadingShotgun()
    {   
        //NOTE:
        //coroutine cagirilirken StartCoroutine kullanilmasi zorunludur
        //normal methodlar gibi cagiramayiz
        StartCoroutine(LoadShotGunCoroutine());
    }

    //NOTE:
    //coroutine ne kadar zamanda donecegini yield return ýle anlar
    //burada ek olarak saniye de verilebilir 
    IEnumerator LoadShotGunCoroutine()
    {
        audioManager.PlayShotgunReloadSFX();
        //NOTE:
        //3 saniye sonra method calismaya baslar 
        yield return new WaitForSeconds(shotgunLoadTime);
        isShotgunLoaded = true;
    }

    public void StopLoadShotgun()
    {
        if (_loadShotgunCoroutine != null)
        {
            //NOTE:
            //Coroutine methodunu calismaya basladi ama durdurmak istedik
            //o nedenle o methodu temsil eden coroutine degiskeni uzerinden StopCoroutine ile methodu durdurduk
            StopCoroutine(_loadShotgunCoroutine);
        }

        audioManager.StopShotgunReloadSFX();
    }

    public void TrySpawnBullets()
    {
        if ((isShotgunLoaded))
        {

            for (int i = 0; i < bulletCount; i++)
            {
                SpawnBullet();
            }
            playerHolder.PushPlayerBack();
            audioManager.PlayShotgunShootSFX();
        }

        isShotgunLoaded = false;
    }
    public void SpawnBullet()
    {
        //create randomly geenrated Vector3 for a spread of the bullet 
        var spread = new Vector3(Random.Range(-maxSpread, maxSpread),
                                 Random.Range(-maxSpread * .4f, maxSpread * .4f),
                                 Random.Range(-maxSpread, maxSpread));
        //NOTE:
        //Instantiate prefab kullanilarak oyun sahnesine yeni bir nesne eklemek
        //icin kullanilan methoddur. Gerekliyse transform degeri de parametre olarak eklenebilir.
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletSpawnPoint.position;
        //NOTE:
        //forward VEKTORU spawn pointe EKLENEREK bulletin dogru yerden olusmasi saglanir (vektor toplamayi dusun)
        //eklemezsek surekli sifira yakin bir VEKTOR degeri position olarak algilanir ve 0,0,0 yakin bir yerden olusur
        //spread ekleyerek de olusan mermilerin olabilidigince farklý yonlere doðru olusmasi ve shotgun goruntusu 
        //vermesini istedik
        newBullet.transform.LookAt(bulletSpawnPoint.position + bulletSpawnPoint.forward + spread);
    }


    public void LevelCompleted()
    {
        ingameControlsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        winUI.Show();
    }    

    public void DiamondCollected()
    {   
        
         LevelCompleted();
    }
}
