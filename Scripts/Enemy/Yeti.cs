using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Yeti : MonoBehaviour
{
    [SerializeField] EnemyAudio yetiAudio;
    [SerializeField] EnemyStats yetiStats;
    [SerializeField] GameObject target;
    [SerializeField] GameObject hitbox;
    GameObject deathSprite;
    float forgetPlayerTime, docileSpeed, hostileSpeed, attackSpeed, spawnPickupChance, destinationDistance;
    float timeLeftToForgetPlayer, attackCooldown = 0f;
    bool hasBeenSeen = false, isDead = false;
    AIPath aIPath;
    Vector2 destination, lookDir;
    Rigidbody2D rb;
    EnemyFOV enemyFOV;
    Animator animator;
    GenericEnemyBehaviour geb;
    bool playRunAwaySound = true, playAlertSound = true;

    // Start is called before the first frame update
    void Start()
    {

        SetYetiStatsFromScriptableObject();
        aIPath = GetComponent<AIPath>();
        geb = FindObjectOfType<GenericEnemyBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        enemyFOV = GetComponent<EnemyFOV>();
        animator = GetComponentInParent<Animator>();
        destination = target.transform.position;
        timeLeftToForgetPlayer = forgetPlayerTime;
        aIPath.maxSpeed = docileSpeed;
        hitbox.GetComponent<BoxCollider2D>().enabled = false;

        yetiAudio.EnemySpawnSound(geb.player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        ReachedDestination();
        RunAway();
    }

    void SetYetiStatsFromScriptableObject()
    {
        destinationDistance = yetiStats.yetiDestinationDistance;
        deathSprite = yetiStats.yetiDeathSprite;
        forgetPlayerTime = yetiStats.yetiForgetPlayerTime;
        docileSpeed = yetiStats.yetiDocileSpeed;
        hostileSpeed = yetiStats.yetiHostileSpeed;
        attackSpeed = yetiStats.yetiAttackSpeed;
        spawnPickupChance = yetiStats.yetiSpawnPickupChance;
    }

    void RunAway()
    {
        if (geb.player.GetComponentInChildren<Torch>().wavingTorch & Vector2.Distance(transform.position, geb.player.transform.position) < 5 & enemyFOV._canSeePlayer)
        {
            yetiAudio.EnemyRunAwaySound(playRunAwaySound);
            playRunAwaySound = false;
            aIPath.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, geb.player.transform.position, -1 * 1 * Time.deltaTime);
            lookDir = (geb.player.transform.position - transform.position).normalized;
            rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }
        else if (Input.GetMouseButton(1) & Vector2.Distance(transform.position, geb.player.transform.position) < 10 & !enemyFOV._canSeePlayer)
        {
            target.transform.position = geb.player.transform.position;
        }
        else
        {
            aIPath.enabled = true;
        }

        if (!geb.player.GetComponentInChildren<Torch>().wavingTorch)
        {
            playRunAwaySound = true;
        }
    }

    void ReachedDestination()
    {
        if (Vector2.Distance(transform.position, destination) <= destinationDistance)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator DealDamage()
    {
        hitbox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        hitbox.GetComponent<BoxCollider2D>().enabled = false;

    }

    void AttackPlayer()
    {
        attackCooldown -= Time.deltaTime;
        if (Vector2.Distance(transform.position, target.transform.position) <= 1 & attackCooldown <= 0)
        {
            yetiAudio.EnemyAttackSound();
            attackCooldown = attackSpeed;
            animator.SetTrigger("attacking");
            StartCoroutine(DealDamage());
        }
    }

    void ChasePlayer()
    {
        if (enemyFOV._canSeePlayer)
        {
            yetiAudio.EnemyAlertSound(playAlertSound);
            playAlertSound = false;
            hasBeenSeen = true;
            target.transform.position = geb.player.transform.position;
            timeLeftToForgetPlayer = forgetPlayerTime;
            aIPath.maxSpeed = hostileSpeed;
            aIPath.endReachedDistance = 2;
            AttackPlayer();
        }
        else if (hasBeenSeen)
        {
            target.transform.position = geb.player.transform.position;
            timeLeftToForgetPlayer -= Time.deltaTime;
            if (timeLeftToForgetPlayer < 0 & Vector2.Distance(transform.position, target.transform.position) >= 1)
            {
                aIPath.maxSpeed = docileSpeed;
                hasBeenSeen = false;
                target.transform.position = destination;
                timeLeftToForgetPlayer = forgetPlayerTime;
                aIPath.endReachedDistance = 0.1f;
                playAlertSound = true;
            }
        }
    }

    void SpawnDeadBody()
    {
        var dead = Instantiate(deathSprite, transform.position, Quaternion.identity);
        var deathForce = (geb.player.transform.position + transform.position).normalized * 3;
        dead.GetComponent<Rigidbody2D>().AddForce(deathForce, ForceMode2D.Impulse);
        dead.GetComponent<Rigidbody2D>().rotation = geb.player.angle;
    }


    void Die()
    {
        isDead = true;
        Destroy(transform.parent.gameObject);
        SpawnDeadBody();
        GetComponent<PickupSpawner>().TrySpawnPickup(spawnPickupChance, transform);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Weapon" & !isDead)
        {
            Die();
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player Weapon" & !isDead)
        {
            Die();
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
