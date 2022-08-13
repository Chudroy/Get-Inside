using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class GlobalLightBehaviour : MonoBehaviour
{
    [SerializeField] bool lightOn = false;
    [SerializeField] float lightIntensity;
    Light2D light2D;
    void Start()
    {
        light2D = GetComponent<Light2D>();
        gameObject.SetActive(lightOn);
        light2D.intensity = lightIntensity;
    }
}
