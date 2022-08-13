using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoDisplay : MonoBehaviour
{
    float ammoCount;
    void Update()
    {
        GetAmmoCount();
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        GetComponent<Text>().text = ammoCount.ToString();
    }

    void GetAmmoCount()
    {
        if(tag == "Player Flaregun")
        {
            ammoCount = FindObjectOfType<PlayerResourceManager>().playerFlaregunAmmo;

        } else if (tag == "Player Ricochet Gun")
        {
            ammoCount = FindObjectOfType<PlayerResourceManager>().playerRicochetGunAmmo;
        }
    }
}
