using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [Header("Elements")]
    public AudioSource shotgunReloadAS;
    public AudioSource shotgunShootAS;
    public AudioSource metalImpactAS;
    public AudioSource healAS;

    public void PlayShotgunReloadSFX()
    {
        shotgunReloadAS.Play();
    }

    public void StopShotgunReloadSFX()
    {
        shotgunReloadAS.Stop();
    }

    public void PlayShotgunShootSFX()
    {
        shotgunShootAS.Play();
    }

    public void PlayMetalImpactSFX()
    {
        metalImpactAS.Play();
    }


    public void PlayHealSFX()
    {
        healAS.Play();
    }
}
