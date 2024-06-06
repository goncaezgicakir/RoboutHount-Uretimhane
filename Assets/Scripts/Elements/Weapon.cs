using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    [Header("Managers")]
    public Settings settings;

    [Header("Elements")]
    public Player player;
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    public Material trajectoryLoadedMaterial;
    public Material trajectoryUnloadedMaterial;
    public ParticleSystem muzzlePS;
    
    [Header("Properties")]
    public bool isShotgunLoaded;
    public List<MeshRenderer> trajectoryMeshRenderers;
    //NOTE:
    //Coroutine degiskeni ile coroutine methodu resultina yeniden erisebilriz
    private Coroutine _loadShotgunCoroutine;



    public void StartLoadingShotgunCoroutine()
    {
        //NOTE:
        //coroutine cagirilirken StartCoroutine kullanilmasi zorunludur
        //normal methodlar gibi cagiramayiz
        _loadShotgunCoroutine = StartCoroutine(LoadShotGunCoroutine());
    }

    //NOTE:
    //coroutine ne kadar zamanda donecegini yield return ýle anlar
    //burada ek olarak saniye de verilebilir 
    IEnumerator LoadShotGunCoroutine()
    {
        player.gameDirector.audioManager.PlayShotgunReloadSFX();
        //NOTE:
        //3 saniye sonra method calismaya baslar 
        yield return new WaitForSeconds(player.gameDirector.settings.shotgunLoadTime);
        isShotgunLoaded = true;
        ChangeTrajectoryMaterialsToLoaded();
    }

    public void StopLoadShotgunCoroutine()
    {
        if (_loadShotgunCoroutine != null)
        {
            //NOTE:
            //Coroutine methodunu calismaya basladi ama durdurmak istedik
            //o nedenle o methodu temsil eden coroutine degiskeni uzerinden StopCoroutine ile methodu durdurduk
            StopCoroutine(_loadShotgunCoroutine);
        }

        player.gameDirector.audioManager.StopShotgunReloadSFX();
    }

    public void TrySpawnBullets()
    {
       
        if ((isShotgunLoaded))
        {
            for (int i = 0; i < settings.bulletCount; i++)
            {
                SpawnBullet();
            }
            player.PushPlayerBack();
            player.gameDirector.enemyManager.AlertEnemies();
            player.gameDirector.audioManager.PlayShotgunShootSFX();
        }

        isShotgunLoaded = false;
        muzzlePS.Play();
        ChangeTrajectoryMaterialsToUnloaded();
    }

    public void SpawnBullet()
    {
        //create randomly geenrated Vector3 for a spread of the bullet 
        var maxSpread = settings.maxSpread;

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
   
    public void ChangeTrajectoryMaterialsToLoaded()
    {
        foreach(var mr in trajectoryMeshRenderers)
        {
            mr.material = trajectoryLoadedMaterial;
            
            //NOTE:
            //bu objenin ilk scale degeri ile tween kullanýlarak
            //kuculup buyumesini saglariz (nefes alma animasyonu gibi)
            var startScale = mr.transform.localScale;
            mr.transform.DOScale(startScale * 1.5f, .5f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void ChangeTrajectoryMaterialsToUnloaded()
    {
        foreach (var mr in trajectoryMeshRenderers)
        {
            mr.material = trajectoryUnloadedMaterial;
            mr.transform.DOKill();
            mr.transform.localScale = new Vector3(1, 1, .17f);
        }
    }
}
