using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemyStats", menuName = "ScriptableObjects/enemy/enemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header("Yeti")]
    public float yetiDestinationDistance;
    public GameObject yetiDeathSprite;
    public float yetiForgetPlayerTime;
    public float yetiDocileSpeed;
    public float yetiHostileSpeed;
    public float yetiAttackSpeed;
    public float yetiSpawnPickupChance;
    [Header("Alien")]
    public GameObject alienDeathSprite;
    public GameObject alienProjectile;
    public float alienForgetPlayerTime;
    public float alienDocileSpeed;
    public float alienHostileSpeed;
    public float alienAttackSpeed;
    public float alienRunAwayDuration;
    public float alienSpawnPickupChance;
    [Header("Robot")]
    public GameObject robotDeadPrefab;
    public float robotForgetPlayerTime;
    public float robotDocileSpeed;
    public float robotAttackSpeed;
    public float robotLaserSpeed;
    public float robotScanSpeed;
    public float robotSpawnPickupChance;
    [Header("Ghost")]
    public float forgetPlayerTime;
    public float docileSpeed;
    public float hostileSpeed;
}
