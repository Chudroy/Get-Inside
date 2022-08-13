using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class LightGlow : MonoBehaviour
{
    Light2D glowingLight;
    [SerializeField] float maxIntensity = 1;
    [SerializeField] float minIntensity = 0;
    [SerializeField] float intensitySpeed = 1;
    float currentIntensity = 0;
    // Start is called before the first frame update
    void Start()
    {
        glowingLight = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Glow();
    }

    void Glow()
    {
        glowingLight.intensity = currentIntensity;

        if (currentIntensity > maxIntensity || currentIntensity < minIntensity)
        {
            intensitySpeed *= -1;
        }

        currentIntensity += Time.deltaTime * intensitySpeed;

    }
}
