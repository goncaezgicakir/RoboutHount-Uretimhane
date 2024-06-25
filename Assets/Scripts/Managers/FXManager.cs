using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem explosionPSPrefab;
    public ParticleSystem healCollectedPSPrefab;

    public void PlayExplosionFX(Vector3 position)
    {
        var newPS = Instantiate(explosionPSPrefab);
        newPS.transform.position = position;
    }

    public void PlayHealCollectedFX(Vector3 position)
    {
        var newPS = Instantiate(healCollectedPSPrefab);
        newPS.transform.position = position;
    }

}
