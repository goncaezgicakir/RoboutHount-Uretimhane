using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem explosionPS;

    public void PlayExplosionFX(Vector3 position)
    {
        var newPS = Instantiate(explosionPS);
        newPS.transform.position = position;
    }

}
