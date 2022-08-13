using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AlienProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float followPlayerDuration;
    [SerializeField] bool followingPlayer;
    GenericEnemyBehaviour geb;
    float timeLeft;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        geb = FindObjectOfType<GenericEnemyBehaviour>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        // spriteRenderer.color = new Color(0,0,0,50);
        timeLeft = followPlayerDuration;
        rb = GetComponent<Rigidbody2D>();
        followingPlayer = true;
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (followingPlayer & geb.player != null)
        {
            Vector2 desired = (geb.player.transform.position - transform.position).normalized * projectileSpeed;
            var force = desired - rb.velocity;
            rb.AddForce(force, ForceMode2D.Force);
            timeLeft -= Time.deltaTime;
        }

        if(timeLeft <= 0)
        {
            followingPlayer = false;
        }
    }

    public void DetachFromParent()
    {
        transform.parent = null;
    }
}
