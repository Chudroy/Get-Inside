using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject instructions;
    [HideInInspector] public bool inventoryOpen, instructionsOpen;
    void Update()
    {
        ToggleInventory();
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory.activeInHierarchy == false & instructionsOpen == false)
            {
                inventory.SetActive(true);
                inventoryOpen = true;
                HUD.SetActive(false);
                Time.timeScale = 0;
            }
            else if (inventory.activeInHierarchy == true)
            {
                inventory.SetActive(false);
                inventoryOpen = false;
                HUD.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }

    public void OpenInstructions()
    {
        Debug.Log("Open Instructions");
        instructions.SetActive(true);
        inventory.SetActive(false);
        instructionsOpen = true;
    }

    public void CloseInstructions()
    {
        instructions.SetActive(false);
        inventory.SetActive(true);
        instructionsOpen = false;
    }
}
