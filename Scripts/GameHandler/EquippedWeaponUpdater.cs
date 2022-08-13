using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquippedWeaponUpdater : MonoBehaviour
{
    [SerializeField] Sprite axeSprite;
    [SerializeField] Sprite flaregunSprite;
    [SerializeField] Sprite ricochetGunSprite;
    Image currentWeaponSprite;
    Text currentWeaponAmmoText;
    PlayerResourceManager playerResourceManager;
    void Awake()
    {
        currentWeaponSprite = transform.GetChild(0).GetComponent<Image>();
        currentWeaponAmmoText = transform.GetChild(1).GetComponent<Text>();
        currentWeaponSprite.sprite = axeSprite;
        playerResourceManager = FindObjectOfType<PlayerResourceManager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoText();
    }
    void UpdateAmmoText()
    {

        if (FindObjectOfType<Player>().gameObject.transform.parent.tag == "Player Flaregun")
        {
            EnableAmmoTextIfDisabled();
            currentWeaponAmmoText.text = playerResourceManager.playerFlaregunAmmo.ToString();
        }
        else if (FindObjectOfType<Player>().gameObject.transform.parent.tag == "Player Axe")
        {
            currentWeaponAmmoText.enabled = false;
        }
        else if (FindObjectOfType<Player>().gameObject.transform.parent.tag == "Player Ricochet Gun")
        {
            EnableAmmoTextIfDisabled();
            currentWeaponAmmoText.text = playerResourceManager.playerRicochetGunAmmo.ToString();
        }
    }

    void EnableAmmoTextIfDisabled()
    {
        if (currentWeaponAmmoText.enabled == false)
        {
            currentWeaponAmmoText.enabled = true;
        }
    }

    public void UpdateWeaponSprite(int weaponIdx)
    {
        switch (weaponIdx)
        {
            case 0:
                currentWeaponSprite.sprite = flaregunSprite;
                break;
            case 1:
                currentWeaponSprite.sprite = axeSprite;
                break;
            case 2:
                currentWeaponSprite.sprite = ricochetGunSprite;
                break;
            default:
                currentWeaponSprite.sprite = null;
                break;
        }

    }
}
