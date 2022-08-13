using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public Player player;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();

    }
    void Start()
    {
    }
    public void FindInstanceOfPlayer()
    {
        player = FindObjectOfType<Player>();
    }
}
