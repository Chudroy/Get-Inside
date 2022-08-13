using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnersController : MonoBehaviour
{
    [SerializeField] float controllerTimeBetweenSpawns = 0.1f;
    [SerializeField] float controllerProbability = 0.2f;
    [SerializeField] int maxNumberOfEnemies = 3;
    [SerializeField] bool enemyNumberLimitReached;
    public bool _enemyNumberLimitReached { get { return enemyNumberLimitReached; } }
    Spawner[] spawners;
    float yetiSum;
    
    

    // Start is called before the first frame update
    void Start()
    {
        spawners = transform.GetComponentsInChildren<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSpawnerVariables();
        CalculateEnemySum();
        LimitNumberOfEnemies();
    }

    void SetSpawnerVariables()
    {
        
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].timeBetweenSpawns = controllerTimeBetweenSpawns;
            spawners[i].probability = controllerProbability;
        }
    }
    void LimitNumberOfEnemies()
    {
        if(ReachedEnemyMaxNum())
        {   
            enemyNumberLimitReached = true;
        } else
        {
            enemyNumberLimitReached = false;
        }
    }
    bool ReachedEnemyMaxNum()
    {
        return yetiSum >= maxNumberOfEnemies;
    }
    void CalculateEnemySum()
    {
        float currentSum = 0;

        for (int i = 0; i < spawners.Length; i++)
        {
            currentSum += spawners[i]._spawnedEnemies;
        }

        yetiSum = currentSum;
    }
}
