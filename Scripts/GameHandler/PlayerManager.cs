using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerType currentPlayerType;
    public void GetCurrentPlayerType()
    {
        currentPlayerType = FindObjectOfType<Player>().playerType;
    }

    public void LoadPlayer()
    {
        if (currentPlayerType == PlayerType.Axe)
        {
            transform.GetChild(0).GetComponent<PlayerSelector>().OnPlayerAxeSelect();
        }
        else if (currentPlayerType == PlayerType.FlareGun)
        {
            transform.GetChild(0).GetComponent<PlayerSelector>().OnPlayerFlareGunSelect();

        }
        else if (currentPlayerType == PlayerType.RicochetGun)
        {
            transform.GetChild(0).GetComponent<PlayerSelector>().OnPlayerRicochetGunSelect();
        }

    }
}
