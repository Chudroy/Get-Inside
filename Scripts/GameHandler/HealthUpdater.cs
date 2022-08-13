using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUpdater : MonoBehaviour
{
    Slider slider;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        currentHealth = FindObjectOfType<PlayerResourceManager>().playerHealth;
        slider.value = currentHealth / 100;
    }


}
