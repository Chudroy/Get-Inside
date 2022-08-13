using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestExit : MonoBehaviour
{
    LevelLoader levelLoader;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
    }
}
