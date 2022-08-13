using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerStatsScriptableObject playerStats;
    [HideInInspector] public float playerWalkSpeed;
    [HideInInspector] public float playerSprintSpeed;
    [HideInInspector] public float playerRecoverySpeed;
    [HideInInspector] public float playerStamina;
    [HideInInspector] public float playerEnergy;
    void Start()
    {
        SetPlayerStats();
        playerEnergy = playerStamina;
    }
    public void PlayerStatsReset()
    {
        playerEnergy = playerStamina;
    }

    void SetPlayerStats()
    {
        playerWalkSpeed = playerStats.playerPrefabWalkSpeed;
        playerSprintSpeed = playerStats.playerPrefabSprintSpeed;
        playerRecoverySpeed = playerStats.playerPrefabRecoverySpeed;
        playerStamina = playerStats.playerPrefabStamina;
    }
}
