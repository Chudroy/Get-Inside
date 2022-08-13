using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/player/playerPrefabs")]

public class PlayerPrefabs : ScriptableObject
{
    public GameObject playerAxe;
    public GameObject playerFlareGun;
    public GameObject playerRicochetGun;
}
