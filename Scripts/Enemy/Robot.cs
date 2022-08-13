using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Robot : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] EnemyAudio robotAudio;
    [SerializeField] ScanningEnemyAudio robotScanAudio;
    [SerializeField] EnemyStats robotStats;
    [SerializeField] GameObject target;
    [SerializeField] GameObject robotLaserPrefab;
    GameObject robotDeadPrefab;
    float forgetPlayerTime, docileSpeed, attackSpeed, laserSpeed, scanSpeed, spawnPickupChance, timeLeftToForgetPlayer;
    GenericEnemyBehaviour geb;
    Transform robotHead, robotBody, robotLaser;
    AIPath aIPath;
    Vector2 destination;
    Rigidbody2D rb, robotHeadRigidBody;
    EnemyFOV enemyFOV;
    Animator animator;
    bool isDead = false, scanning = false, playAlertSound = true, playDetectedSound = true;
    float attackCooldown = 0f;
    float scanStartAngle;
    float rotation = 0f;
    float shootDelay = 1f;

    void Start()
    {
        SetRobotStatsFromScriptableObject();
        geb = FindObjectOfType<GenericEnemyBehaviour>();
        robotAudio.EnemySpawnSound(geb.player.transform.position);
        aIPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        robotHead = transform.GetChild(0);
        robotBody = transform.GetChild(1);
        robotLaser = robotHead.GetChild(0);
        enemyFOV = GetComponent<EnemyFOV>();
        robotHeadRigidBody = robotHead.GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        destination = target.transform.position;
        timeLeftToForgetPlayer = forgetPlayerTime;
        aIPath.maxSpeed = docileSpeed;
        scanStartAngle = enemyFOV._angle;
        rotation = scanStartAngle;
    }
    void Update()
    {
        ReachedDestination();
        if (!isDead)
        {
            Scan();
        }
    }

    void SetRobotStatsFromScriptableObject()
    {
        robotDeadPrefab = robotStats.robotDeadPrefab;
        forgetPlayerTime = robotStats.robotForgetPlayerTime;
        docileSpeed = robotStats.robotDocileSpeed;
        attackSpeed = robotStats.robotAttackSpeed;
        laserSpeed = robotStats.robotLaserSpeed;
        scanSpeed = robotStats.robotScanSpeed;
        spawnPickupChance = robotStats.robotSpawnPickupChance;
    }
    void Shoot()
    {
        if (attackCooldown <= 0)
        {
            robotScanAudio.PlayDetectedSound(playDetectedSound, playOneShot: true);
            robotAudio.EnemyAttackSound();
            attackCooldown = attackSpeed;
            var robotLaser = Instantiate(robotLaserPrefab, transform.position, Quaternion.identity);
            var laserDir = (geb.player.transform.position - robotLaser.transform.position).normalized;
            var laserForce = laserDir * laserSpeed;
            robotLaser.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(laserDir.y, laserDir.x) * Mathf.Rad2Deg + 90f;
            robotLaser.GetComponent<Rigidbody2D>().AddForce(laserForce, ForceMode2D.Impulse);
        }
    }

    void Scan()
    {

        if (enemyFOV._canSeePlayer & !scanning)
        {
            robotAudio.EnemyAlertSound(playAlertSound, playOneShot: true);
            playAlertSound = false;
            aIPath.enabled = false;
            scanning = true;
            robotHead.transform.localEulerAngles = new Vector3(0, 0, scanStartAngle);
        }
        if (scanning & enemyFOV._canSeePlayer)
        {
            attackCooldown -= Time.deltaTime;
            robotLaser.gameObject.SetActive(true);
            rotation -= scanSpeed * Time.deltaTime;
            robotHead.transform.localEulerAngles = new Vector3(0, 0, rotation);

            if (robotHead.GetComponent<EnemyFOV>()._canSeePlayer & shootDelay <= 0)
            {
                Shoot();
            }
            if (robotHead.transform.localEulerAngles.z > scanStartAngle & robotHead.transform.localEulerAngles.z < 120)
            {
                scanSpeed *= -1;
            }
            else if (robotHead.transform.localEulerAngles.z > 230 & robotHead.transform.localEulerAngles.z < 270)
            {
                scanSpeed *= -1;
            }

            shootDelay -= Time.deltaTime;
        }
        else if (scanning & !enemyFOV._canSeePlayer)
        {
            robotScanAudio.PlayStopScanningSound();
            playAlertSound = true;
            robotLaser.gameObject.SetActive(false);
            robotHead.transform.localEulerAngles = Vector3.zero;
            scanning = false;
            aIPath.enabled = true;
            rotation = scanStartAngle;
            shootDelay = 1f;
        }
    }

    void ReachedDestination()
    {
        if (Vector2.Distance(transform.position, destination) <= 2)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        Instantiate(explosion, transform.position, Quaternion.identity);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        Instantiate(robotDeadPrefab, transform.position, Quaternion.identity);
        GetComponent<PickupSpawner>().TrySpawnPickup(spawnPickupChance, transform);
        Destroy(transform.parent.gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Weapon")
        {
            StartCoroutine(Die());
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player Weapon")
        {
            if (!isDead)
            {
                isDead = true;
                StartCoroutine(Die());
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
