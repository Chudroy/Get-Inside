using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] PickupsListScriptableObject pickups;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TrySpawnPickup(float chance, Transform enemyTransform)
    {
        var r = Random.Range(0, 1f);
    
        if (r < chance)
        {
            SpawnPickup(enemyTransform);
        }
    }

    void SpawnPickup(Transform enemyTransform)
    {
        var r = Random.Range(0, 1f);

        if (r < 0.33f)
        {
            var p = Instantiate(pickups.RicochetAmmoPackPrefab, enemyTransform.position, Quaternion.identity);
            AddForceToPickup(p);
        }
        else if (r < 0.66f)
        {
            var p = Instantiate(pickups.flareGunAmmoPackPrefab, enemyTransform.position, Quaternion.identity);
            AddForceToPickup(p);
        }
        else if (r < 1)
        {
            var p = Instantiate(pickups.healthpackPrefab, enemyTransform.position, Quaternion.identity);
            AddForceToPickup(p);
        }
    }

    void AddForceToPickup(GameObject go)
    {
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 3f), Random.Range(0, 3f)), ForceMode2D.Impulse);
    }
}
