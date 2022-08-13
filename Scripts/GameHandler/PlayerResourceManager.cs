using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerResourceManager : MonoBehaviour
{
    [SerializeField] GameObject restartTextHandler;
    public float playerHealth = 100;
    public float playerMaxHealth = 100;
    public float playerFlaregunAmmo;
    public float playerRicochetGunAmmo;
    const string playerFlareGunAmmoString = "flareGunAmmo";
    const string playerRicochetGunAmmoString = "ricochetGunAmmo";
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerPrefs.SetFloat(playerFlareGunAmmoString, 0);
            PlayerPrefs.SetFloat(playerRicochetGunAmmoString, 0);
        }
        else
        {
            playerFlaregunAmmo = PlayerPrefs.GetFloat(playerFlareGunAmmoString, 0);
            playerRicochetGunAmmo = PlayerPrefs.GetFloat(playerRicochetGunAmmoString, 0);
        }

    }
    void Update()
    {
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        HealthRegen();

        if (playerHealth <= 1)
        {
            PlayerDie();
        }
    }
    void PlayerDie()
    {
        var currentPlayer = FindObjectOfType<Player>();
        currentPlayer.isDead = true;
        currentPlayer.transform.parent.GetComponent<PlayerAnimationController>().isDead = true;
        restartTextHandler.SetActive(true);
    }

    public void SaveResourceValues()
    {
        PlayerPrefs.SetFloat("flareGunAmmo", playerFlaregunAmmo);
        PlayerPrefs.SetFloat("ricochetGunAmmo", playerRicochetGunAmmo);
    }

    public void PlayerHealthReset()
    {
        playerHealth = 100;
    }
    public void PlayerAmmoReset(int buildIndex)
    {
        if (buildIndex != 1)
        {
            playerFlaregunAmmo = PlayerPrefs.GetFloat("flareGunAmmo", 0);
            playerRicochetGunAmmo = PlayerPrefs.GetFloat("ricochetGunAmmo", 0);
        }
        else
        {
            playerFlaregunAmmo = 0;
            playerRicochetGunAmmo = 0;
        }

    }

    public void HealthRegen()
    {
        playerHealth += Time.deltaTime;
    }

    public void SubtractHealth(float healthSubtracted)
    {
        playerHealth -= healthSubtracted;
    }

    public void AddHealth(float healthAdded)
    {
        playerHealth += healthAdded;
    }

    public void AddAmmo(PickupTag pickupTag, int amount)
    {
        if (pickupTag == PickupTag.ricochetAmmo)
        {
            playerRicochetGunAmmo += (float)amount;
        }
        else if (pickupTag == PickupTag.flareAmmo)
        {
            playerFlaregunAmmo += (float)amount;
        }
    }
}
