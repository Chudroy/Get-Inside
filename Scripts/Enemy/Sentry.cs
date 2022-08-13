using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Sentry : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] EnemyAudio sentryAudio;
    [SerializeField] ScanningEnemyAudio sentryScanAudio;
    [SerializeField] GameObject deadSentrySprite;
    [SerializeField] GameObject sentryLaserPrefab;
    [SerializeField] float attackDelay;
    [SerializeField] float laserSpeed = 1f;
    [SerializeField] float scanSpeed = 1f;
    Transform sentryHead, sentryLaser;
    Player player;
    Rigidbody2D rb, sentryHeadRigidBody;
    Animator animator;
    float attackCooldown = 0f;
    float rotation = 0f;
    EnemyFOV sentryHeadFOV;
    bool playAlertSound = true, playDetectedSound = true;

    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        sentryHead = transform.GetChild(0);
        sentryLaser = sentryHead.GetChild(0);
        sentryHeadRigidBody = sentryHead.GetComponent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        sentryHeadFOV = sentryHead.GetComponent<EnemyFOV>();
        // hitbox.GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Scan();
    }

    void FixedUpdate()
    {

    }

    void Shoot()
    {
        if (attackCooldown <= 0)
        {
            sentryScanAudio.PlayDetectedSound(playDetectedSound, playOneShot: true);
            playDetectedSound = false;
            sentryAudio.EnemyAttackSound();
            player = FindObjectOfType<Player>();
            attackCooldown = attackDelay;
            var sentyLaser = Instantiate(sentryLaserPrefab, transform.position, Quaternion.identity, transform);
            var laserDir = (player.transform.position - sentryLaser.transform.position).normalized;
            var laserForce = laserDir * laserSpeed;
            sentyLaser.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(laserDir.y, laserDir.x) * Mathf.Rad2Deg + 90f;
            sentyLaser.GetComponent<Rigidbody2D>().AddForce(laserForce, ForceMode2D.Impulse);
        }
        else
        {
            playDetectedSound = true;
        }
    }

    void Scan()
    {
        attackCooldown -= Time.deltaTime;
        sentryLaser.gameObject.SetActive(true);
        rotation -= scanSpeed * Time.deltaTime;
        sentryHead.transform.localEulerAngles = new Vector3(0, 0, rotation);


        if (sentryHeadFOV._canSeePlayer)
        {
            sentryAudio.EnemyAlertSound(playAlertSound, playOneShot: true);
            playAlertSound = false;
            Shoot();
        }
        else
        {
            playAlertSound = true;
        }
    }

    IEnumerator Die()
    {
        // animator.SetTrigger("isDead");
        GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(deadSentrySprite, transform.position, Quaternion.identity);
        GetComponent<PickupSpawner>().TrySpawnPickup(1, transform);
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
            StartCoroutine(Die());
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
