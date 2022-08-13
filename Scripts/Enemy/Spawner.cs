using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject targetDestination;
    public float timeBetweenSpawns;
    [Range(0, 1)] public float probability;
    [SerializeField] float spawnedEnemies;
    public float _spawnedEnemies { get { return spawnedEnemies; } }
    EnemySpawnersController enemySpawnersController;

    Vector3 spawnLocationRange;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocationRange = transform.localScale / 2.0f;
        enemySpawnersController = transform.GetComponentInParent<EnemySpawnersController>();
        if (enemyPrefab != null)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnedEnemies();
    }

    void UpdateSpawnedEnemies()
    {
        spawnedEnemies = transform.childCount;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            var r = Random.Range(0f, 1f);
            if (r < probability & enemySpawnersController._enemyNumberLimitReached == false)
            {
                var spawnLoc = new Vector2(
                                Random.Range(transform.position.x - spawnLocationRange.x, transform.position.x + spawnLocationRange.x),
                                Random.Range(transform.position.y - spawnLocationRange.y, transform.position.y + spawnLocationRange.y)
                            );

                var newEnemy = Instantiate(enemyPrefab, spawnLoc, Quaternion.identity);
                if (newEnemy != null)
                {
                    newEnemy.transform.SetParent(transform);
                    var destinationLoc = new Vector2(
                                    Random.Range(targetDestination.transform.position.x - spawnLocationRange.x,
                                    targetDestination.transform.position.x + spawnLocationRange.x),
                                    Random.Range(targetDestination.transform.position.y - spawnLocationRange.y
                                    , targetDestination.transform.position.y + spawnLocationRange.y)
                    );
                    newEnemy.transform.GetChild(0).GetComponent<Transform>().position = destinationLoc;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
