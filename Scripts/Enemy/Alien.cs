using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Alien : MonoBehaviour
{
    [SerializeField] EnemyAudio alienAudio;
    [SerializeField] EnemyStats alienStats;
    [SerializeField] LayerMask obstructionMask;
    [HideInInspector] public GameObject deathSprite;
    GameObject alienProjectile;
    [SerializeField] GameObject target;
    [SerializeField] GameObject hitbox;
    float forgetPlayerTime;
    float docileSpeed;
    float hostileSpeed;
    float attackSpeed;
    float runAwayDuration;
    float spawnPickupChance;
    [HideInInspector] public Player player;
    GenericEnemyBehaviour geb;
    AIPath aIPath;
    Vector2 destination, lookDir, runawayLoc;
    Rigidbody2D rb;
    EnemyFOV enemyFOV;
    Animator animator;
    bool hasBeenSeen = false, runningAway = false;
    float timeLeftToForgetPlayer;
    float attackCooldown = 0f;
    float runAwayTime;
    bool playRunAwaySound = true, playAlertSound = true;

    void Start()
    {
        SetAlienStatsFromScriptableObject();
        geb = FindObjectOfType<GenericEnemyBehaviour>();
        alienAudio.EnemySpawnSound(geb.player.transform.position);
        aIPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        enemyFOV = GetComponent<EnemyFOV>();
        destination = target.transform.position;
        timeLeftToForgetPlayer = forgetPlayerTime;
        aIPath.maxSpeed = docileSpeed;
        hitbox.GetComponent<BoxCollider2D>().enabled = false;
        runAwayTime = runAwayDuration;

    }

    void Update()
    {
        ChasePlayer();
        ReachedDestination();
        RunAway();
    }

    void SetAlienStatsFromScriptableObject()
    {
        deathSprite = alienStats.alienDeathSprite;
        alienProjectile = alienStats.alienProjectile;
        forgetPlayerTime = alienStats.alienForgetPlayerTime;
        docileSpeed = alienStats.alienDocileSpeed;
        hostileSpeed = alienStats.alienHostileSpeed;
        attackSpeed = alienStats.alienAttackSpeed;
        runAwayDuration = alienStats.alienRunAwayDuration;
        spawnPickupChance = alienStats.alienSpawnPickupChance;
    }

    void RotateTowardPlayer()
    {
        if (!runningAway)
        {
            aIPath.enabled = false;
            lookDir = (geb.player.transform.position - transform.position).normalized;
            rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }

    }

    void RunAway()
    {
        if (Input.GetMouseButton(1) &
        !Physics2D.Raycast(transform.position, lookDir, Vector2.Distance(transform.position, geb.player.transform.position),
        obstructionMask) &
         enemyFOV._canSeePlayer)
        {
            alienAudio.EnemyRunAwaySound(playRunAwaySound);
            playRunAwaySound = false;
            runningAway = true;
            runAwayTime = runAwayDuration;

        }
        if (runningAway)
        {
            hasBeenSeen = false;
            runawayLoc = transform.position - (transform.position - geb.player.transform.position).normalized * -20;
            target.transform.position = runawayLoc;
            runAwayTime -= Time.deltaTime;
            aIPath.maxSpeed = hostileSpeed;
            if (runAwayTime <= 0)
            {
                playRunAwaySound = true;
                runningAway = false;
                target.transform.position = destination;
                aIPath.maxSpeed = docileSpeed;
                aIPath.endReachedDistance = 0.1f;
            }
        }
    }

    void ReachedDestination()
    {
        if (Vector2.Distance(transform.position, destination) <= 2)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    void AttackPlayer()
    {

        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            alienAudio.EnemyAttackSound(playOneShot: true);
            var proj = Instantiate(alienProjectile, transform.position, Quaternion.identity);
            proj.transform.parent = transform;
            attackCooldown = attackSpeed;
        }
    }

    void ChasePlayer()
    {
        if (enemyFOV._canSeePlayer)
        {
            alienAudio.EnemyAlertSound(playAlertSound, playOneShot: true);
            playAlertSound = false;
            if (Vector2.Distance(transform.position, target.transform.position) < 10)
            {
                AttackPlayer();
                RotateTowardPlayer();
            }
            else
            {
                aIPath.enabled = true;
            }
            hasBeenSeen = true;
            target.transform.position = geb.player.transform.position;
            timeLeftToForgetPlayer = forgetPlayerTime;
            aIPath.maxSpeed = hostileSpeed;
            aIPath.endReachedDistance = 10;

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

    void UnparentProjectiles()
    {
        var projectiles = gameObject.GetComponentsInChildren<AlienProjectile>();
        foreach (AlienProjectile p in projectiles)
        {
            p.DetachFromParent();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Weapon")
        {
            SpawnDeadBody();
            UnparentProjectiles();
            Destroy(transform.parent.gameObject);
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<PickupSpawner>().TrySpawnPickup(spawnPickupChance, transform);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player Weapon")
        {
            SpawnDeadBody();
            UnparentProjectiles();
            Destroy(transform.parent.gameObject);
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(runawayLoc, 1);
    }
}
