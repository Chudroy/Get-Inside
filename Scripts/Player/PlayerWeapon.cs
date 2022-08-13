using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] PlayerAudio playerAudio;
    [SerializeField] GameObject weaponPrefab;
    Player player;
    PlayerResourceManager playerResourceManager;
    UIController uIController;
    GameObject ricochetBullets;
    // Start is called before the first frame update
    void Start()
    {
        playerResourceManager = FindObjectOfType<PlayerResourceManager>();
        uIController = FindObjectOfType<UIController>();
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        UseWeapon();
    }
    public void UseWeapon()
    {
        if (tag == "Player Ricochet Gun")
        {
            UseRicochetGun();
        }
        else if (tag == "Player Flaregun")
        {
            UseFlaregun();
        }
    }

    void UseRicochetGun()
    {
        if (Input.GetMouseButtonDown(0) & weaponPrefab != null & !uIController.inventoryOpen & playerResourceManager.playerRicochetGunAmmo > 0)
        {
            playerAudio.PlayerAttackSound();

            var shotangle = Mathf.Atan2(player._lookDir.y, player._lookDir.x);
            if (shotangle <= 0)
            {
                shotangle = 2 * Mathf.PI + shotangle;
            }

            shotangle *= Mathf.Rad2Deg;
            var shotAngleOffset = 15;
            shotangle -= shotAngleOffset;


            for (int i = 0; i < 10; i++)
            {

                var r = Random.Range(-0.3f, 0.3f);

                var shotangleX = player._lookDir.x - Mathf.Cos(shotangle * Mathf.Deg2Rad);
                var shotangleY = player._lookDir.y - Mathf.Sin(shotangle * Mathf.Deg2Rad);

                var weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
                var weaponForce = new Vector2((player._lookDir.x + shotangleX + r) * player.throwStrength,
                (player._lookDir.y + shotangleY + r) * player.throwStrength);

                var weaponRB = weapon.GetComponent<Rigidbody2D>();
                weaponRB.AddForce(weaponForce, ForceMode2D.Impulse);
                weaponRB.rotation = player.angle;

                shotangle += 3;
            }

            playerResourceManager.playerRicochetGunAmmo--;

        }
    }

    void UseFlaregun()
    {
        if (Input.GetMouseButtonDown(0) & weaponPrefab != null & !uIController.inventoryOpen & playerResourceManager.playerFlaregunAmmo > 0)
        {
            playerAudio.PlayerAttackSound();

            var weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            var weaponForce = new Vector2(player._lookDir.x * player.throwStrength, player._lookDir.y * player.throwStrength);

            var weaponRB = weapon.GetComponent<Rigidbody2D>();
            weaponRB.AddForce(weaponForce, ForceMode2D.Impulse);
            weaponRB.rotation = player.angle;

            playerResourceManager.playerFlaregunAmmo--;
        }
    }
}
