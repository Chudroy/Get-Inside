using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandlerSpawner : MonoBehaviour
{
    [SerializeField] GameObject gameHandlerPrefab;
    GameHandler gameHandlerInstance;
    void Awake()
    {
        gameHandlerInstance = FindObjectOfType<GameHandler>();
        if (gameHandlerInstance == null)
        {
            Instantiate(gameHandlerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
