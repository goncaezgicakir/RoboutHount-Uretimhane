using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    
    [Header("Properties")]
    //NOTE:
    //degerler sahen uzerinde gozlemlenerek set edildi
    public float minDistance = 1.0f;
    public float maxDistance = 6.0f;
    public float smooth = 10.0f;
    
    //NOTE:
    //filmlerde kullanilan camera holder (dolly) gibi
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;


    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }


    private void Update()
    {   
        //kameranýn gitmek istedigi nokta yon vektoru ve maks distance carpimi ile elde edilir
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        
        //kamera bir objeye çarpýyorsa playera göre yeniden mesafelendir (kisaltir)
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            //NOTE:
            //Clamp maks ve min arasina cikan degeri kontrol eder, aþarsa ya da altýnda kalýrsa min-max degerine set eder
            distance = Mathf.Clamp((hit.distance * .8f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
       
        //NOTE:
        //kameranin yeni pozisyonuna gecisi daha 
        //yumusak yapabilmesi icin Lerp kullanilir
        transform.localPosition = Vector3.Lerp(transform.localPosition,
                                                dollyDir * distance,
                                                Time.deltaTime * smooth);
        /*
         
        //NOTE:
        //Unity icinde ekranda isin cizmeye yarar
        //oyun icinde degil sadece editorde gozukur
        Debug.DrawRay(transform.position, transform.parent.position, Color.green);

        */
    }
}
