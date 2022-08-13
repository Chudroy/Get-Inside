using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/pickups/pickupList")]
public class PickupsListScriptableObject : ScriptableObject
{
    public GameObject healthpackPrefab;
    public GameObject RicochetAmmoPackPrefab;
    public GameObject flareGunAmmoPackPrefab;
}
