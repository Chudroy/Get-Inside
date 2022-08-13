using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator legsAnimator;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float axeHitboxEnabledDuration = 0f;
    PlayerAudio playerAudio;
    Animator animator;
    BoxCollider2D axeHitbox;
    bool firstSwing = true, secondSwing = false;
    float attackTime = 0f;
    float axeHitboxEnabledTime;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
        axeHitboxEnabledTime = axeHitboxEnabledDuration;
        animator = GetComponent<Animator>();

        var axe = transform.GetChild(0).transform.Find("Axe Hitbox");
        if (axe != null)
        {
            axeHitbox = axe.GetComponent<BoxCollider2D>();
            axeHitbox.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            SetWalkAnimation();
            AttackAnimation();
            if (axeHitbox != null)
            {
                AxeHitBoxAppearCountdown();
            }
        }
        if (isDead)
        {
            animator.SetTrigger("isDead");
        }
    }

    void SetWalkAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            legsAnimator.SetBool("isWalking", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
            {
                legsAnimator.SetBool("isWalking", false);
            }
        }
    }

    void AttackAnimation()
    {
        attackTime -= Time.deltaTime;

        if (gameObject.tag == "Player Axe")
        {
            if (Input.GetMouseButtonDown(0) & attackTime <= 0)
            {
                playerAudio.PlayerAttackSound();
                if (firstSwing)
                {
                    animator.SetTrigger("First Swing");
                }

                if (secondSwing)
                {
                    animator.SetTrigger("Second Swing");
                }

                var temp = firstSwing;
                firstSwing = secondSwing;
                secondSwing = temp;

                attackTime = attackDelay;
            }
        }
    }

    void EnableAxeHitbox()
    {
        axeHitbox.enabled = true;
        axeHitboxEnabledTime = axeHitboxEnabledDuration;
    }

    void AxeHitBoxAppearCountdown()
    {
        if (axeHitbox.enabled == true)
        {
            axeHitboxEnabledTime -= Time.deltaTime;

            if (axeHitboxEnabledTime <= 0)
            {
                axeHitbox.enabled = false;
            }
        }
    }
}
