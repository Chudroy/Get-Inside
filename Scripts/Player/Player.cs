using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Player : MonoBehaviour
{
    public PlayerType playerType;
    public Camera cam;
    public Vector2 _lookDir { get { return lookDir; } }
    public bool isDead;
    [HideInInspector] public float angle;
    [SerializeField] public float throwStrength = 10;
    PlayerStatsManager statsManager;
    Vector2 mousePos, lookDir;
    Rigidbody2D rb;
    PlayerResourceManager playerResourceManager;
    float playerSpeed;


    void Start()
    {
        statsManager = FindObjectOfType<PlayerStatsManager>();
        playerResourceManager = FindObjectOfType<PlayerResourceManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Sprint();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            PlayerMovement();
            PlayerRotation();
        }
        else
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Sprint()
    {
        statsManager.playerEnergy = Mathf.Clamp(statsManager.playerEnergy, -0.1f, statsManager.playerStamina);
        statsManager.playerEnergy += Time.deltaTime * statsManager.playerRecoverySpeed;

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) & statsManager.playerEnergy >= 0)
        {
            playerSpeed = statsManager.playerSprintSpeed;
            statsManager.playerEnergy -= Time.deltaTime;
        }
        else
        {
            playerSpeed = statsManager.playerWalkSpeed;
        }
    }

    void PlayerRotation()
    {
        lookDir = (mousePos - (Vector2)transform.position).normalized;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    void PlayerMovement()
    {

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {

            rb.MovePosition((Vector2)transform.position + movement * Time.deltaTime * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ReceiveDamage(float enemyAttackPower)
    {
        playerResourceManager.SubtractHealth(enemyAttackPower);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hitbox" || other.tag == "Enemy Projectile")
        {
            ReceiveDamage(other.gameObject.GetComponent<DamageDealer>().attackPower);
        }
        if (other.tag == "Enemy Projectile")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            other.gameObject.GetComponent<PickupBehaviour>().Init();
            Destroy(other.gameObject);
        }


    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ghost"))
        {
            ReceiveDamage(other.gameObject.GetComponent<DamageDealer>().attackPower * Time.deltaTime);
        }
    }
}
