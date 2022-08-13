using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/player/playerStats")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public float playerPrefabWalkSpeed = 5f;
    public float playerPrefabSprintSpeed = 8f;
    public float playerPrefabStamina = 1f;
    public float playerPrefabRecoverySpeed = 0.4f;
    public float playerPrefabWaveTorchIntensity = 2f;
    public float playerPrefabWaveTorchDuration = 1.5f;
    public float playerPrefabTorchRechargeDuration = 5f;
}
