using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaUpdater : MonoBehaviour
{
    Slider slider;
    float currentStamina;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();
    }

    void UpdateStamina()
    {
        currentStamina = FindObjectOfType<PlayerStatsManager>().playerEnergy;
        slider.value = currentStamina;
    }


}
