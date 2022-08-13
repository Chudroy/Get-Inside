using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitAudio : MonoBehaviour
{
    [SerializeField] bool loopAttackSound;
    [SerializeField] int hitLayer;
    [SerializeField] EnemyAudio enemyAudio;
    BoxCollider2D hitbox;
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == hitLayer)
        {
            enemyAudio.EnemyAttackSound(looping:loopAttackSound);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == hitLayer)
        {
            enemyAudio.StopAudio();
        }
    }
}
