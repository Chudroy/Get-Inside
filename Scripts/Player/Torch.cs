using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class Torch : MonoBehaviour
{
    TorchAudio torchAudio;
    public PlayerStatsScriptableObject playerStats;
    Light2D light2D;
    float waveTorchIntensity;
    float waveTorchDuration;
    float rechargeDuration;
    float waveTorchTime;
    float rechargeTime;
    float defaultIntensity;
    public bool recharged = true;
    bool recharging = false;
    public bool wavingTorch;


    [SerializeField] float lightIntensityUp;
    [SerializeField] float lightIntensityDown;

    void Start()
    {

        torchAudio = GetComponent<TorchAudio>();

        light2D = GetComponent<Light2D>();
        defaultIntensity = light2D.intensity;

        SetPlayerTorchStats();

        waveTorchTime = waveTorchDuration;
        rechargeTime = rechargeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        WaveTorch();
        RechargeTorch();
    }

    void SetPlayerTorchStats()
    {
        waveTorchIntensity = playerStats.playerPrefabWaveTorchIntensity;
        waveTorchDuration = playerStats.playerPrefabWaveTorchDuration;
        rechargeDuration = playerStats.playerPrefabTorchRechargeDuration;
    }

    void WaveTorch()
    {
        if (Input.GetMouseButtonDown(1) & recharged)
        {
            wavingTorch = true;
            recharged = false;
            recharging = true;
            torchAudio.PlayTorchWaveSound();
       
        }
        if (wavingTorch)
        {
            light2D.intensity += lightIntensityUp * Time.deltaTime;;
            light2D.intensity = Mathf.Clamp(light2D.intensity, 1, waveTorchIntensity);
        }
        if (light2D.intensity >= 2)
        {
            torchAudio.fadeOut = true;
            waveTorchTime -= Time.deltaTime;
        }
        if (waveTorchTime <= 0)
        {
            wavingTorch = false;
            light2D.intensity -= lightIntensityDown * Time.deltaTime;;
        }
        if (light2D.intensity <= 1f)
        {
            light2D.intensity = 1f;
            waveTorchTime = waveTorchDuration;
        }
    }

    void RechargeTorch()
    {
        if (recharging)
        {
            rechargeTime -= Time.deltaTime;
            if (rechargeTime <= 0)
            {
                recharged = true;
                recharging = false;
                rechargeTime = rechargeDuration;
            }
        }
    }
}
