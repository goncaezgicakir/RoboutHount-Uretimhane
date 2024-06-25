using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Settings : MonoBehaviour
{
    [Header("Player Settings")]
    public bool shootingAlarmEnemies;
    public float playerSpeed;
    public float jumpForce;
    public float shotgunLoadTime;
    public int bulletCount;
    public float maxSpread;

    [Header("PowerUp Settings")]
    public float healSpawnChange;
}
