using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public AudioSource shotgunReloadAS;
    public AudioSource shotgunShootAS;
    public AudioSource metalImpactAS;

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

    public void StopShotgunShootSFX()
    {
        shotgunShootAS.Stop();
    }

    public void PlayMetalImpactSFX()
    {
        metalImpactAS.Play();
    }

    public void StopMetalImpacttSFX()
    {
        metalImpactAS.Stop();
    }
}
