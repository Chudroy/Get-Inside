using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public PickupTag pickupTag;
    [SerializeField] int amount;
    // Start is called before the first frame update
    public void Init()
    {
        if (pickupTag == PickupTag.flareAmmo || pickupTag == PickupTag.ricochetAmmo)
        {
            FindObjectOfType<PlayerResourceManager>().AddAmmo(pickupTag, amount);
        }
        else if (pickupTag == PickupTag.healthPack)
        {
            FindObjectOfType<PlayerResourceManager>().AddHealth((float)amount);
        }
    }

}
