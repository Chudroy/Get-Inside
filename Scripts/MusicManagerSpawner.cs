using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerSpawner : MonoBehaviour
{
    [SerializeField] GameObject musicManagerPrefab;
    MusicManager musicManagerInstance;
    void Awake()
    {
        musicManagerInstance = FindObjectOfType<MusicManager>();
        if (musicManagerInstance == null)
        {
            Instantiate(musicManagerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
