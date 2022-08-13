using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Ghost : MonoBehaviour
{
    [SerializeField] EnemyAudio ghostAudio;
    [SerializeField] EnemyStats ghostStats;
    [SerializeField] GameObject target;
    float forgetPlayerTime = 5f;
    float docileSpeed = 3f;
    float hostileSpeed = 2f;
    Vector2 lookDir;
    Vector2 destination;
    Rigidbody2D rb;
    EnemyFOV enemyFOV;
    Animator animator;
    GenericEnemyBehaviour geb;
    bool interested = true, playAlertSound = true;
    float timeLeftToForgetPlayer;

    void Start()
    {
        SetGhostStatsFromScriptableObject();
        geb = FindObjectOfType<GenericEnemyBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        enemyFOV = GetComponent<EnemyFOV>();
        animator = GetComponentInParent<Animator>();
        destination = target.transform.position;
        timeLeftToForgetPlayer = forgetPlayerTime;
    }

    void Update()
    {
        ReachedDestination();
    }

    void SetGhostStatsFromScriptableObject()
    {
        forgetPlayerTime = ghostStats.forgetPlayerTime;
        docileSpeed = ghostStats.docileSpeed;
        hostileSpeed = ghostStats.hostileSpeed;
    }

    void FixedUpdate()
    {
        if (!enemyFOV._canSeePlayer || !interested)
        {
            GoToDestination();
        }
        else if (enemyFOV._canSeePlayer & interested)
        {
            ChasePlayer();
        }
    }
    void ChasePlayer()
    {
        target.transform.position = geb.player.transform.position;
        ghostAudio.EnemyAlertSound(playAlertSound);
        playAlertSound = false;

        var force = (target.transform.position - transform.position).normalized;
        rb.velocity = force * Time.deltaTime * hostileSpeed;

        RotateTowardTarget(target.transform.position);

        timeLeftToForgetPlayer -= Time.deltaTime;
        if (timeLeftToForgetPlayer <= 0)
        {
            timeLeftToForgetPlayer = forgetPlayerTime;
            interested = false;
            ghostAudio.EnemyRunAwaySound(playSound: true);
        }


    }

    void GoToDestination()
    {
        var force = (destination - (Vector2)transform.position).normalized;
        if (interested)
        {
            rb.velocity = force * Time.deltaTime * docileSpeed;
        }
        else if (!interested)
        {
            rb.velocity = force * Time.deltaTime * hostileSpeed * 2;
        }
        RotateTowardTarget(destination);
    }

    void RotateTowardTarget(Vector2 target)
    {
        lookDir = (target - (Vector2)transform.position).normalized;
        rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    }
    void ReachedDestination()
    {
        if (Vector2.Distance(transform.position, destination) <= 2)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
